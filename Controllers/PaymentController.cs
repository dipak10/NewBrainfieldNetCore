using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NewBrainfieldNetCore.Common.Requests;
using NewBrainfieldNetCore.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace NewBrainfieldNetCore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration config;
        private HttpWebRequest webRequest;
        private string transid, status, respcode, globalreqfrom = string.Empty;

        public string OrderId { get; set; }
        public int UserId { get; set; }
        public Root MyDeserializedClass { get; set; }

        public Dictionary<string, object> Body { get; set; }

        public Dictionary<string, string> Head { get; set; }

        public Dictionary<string, object> RequestBody { get; set; }

        public Dictionary<string, string> UserInfo { get; set; }

        public Dictionary<string, string> TxnAmount { get; set; }

        public string MID { get; set; }
        public string Mkey { get; set; }
        public string PaytmURL { get; set; }
        public string CallbackUrl { get; set; }

        public PaymentController(IConfiguration config)
        {
            this.config = config;
            Body = new Dictionary<string, object>();
            Head = new Dictionary<string, string>();
            RequestBody = new Dictionary<string, object>();
            UserInfo = new Dictionary<string, string>();
            TxnAmount = new Dictionary<string, string>();
            MyDeserializedClass = new Root();
            SetEnvironmentValues();
        }

       

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                GenrateOrderNo();

                Dictionary<string, object> body = GenerateParameters();

                string paytmChecksum = Paytm.Checksum.generateSignature(JsonConvert.SerializeObject(body), Mkey);

                Head.Add("signature", paytmChecksum);

                RequestBody.Add("body", body);
                RequestBody.Add("head", Head);

                SendRequest();

                GetRequestResponse();

                return View(MyDeserializedClass);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home");
            }
            finally
            {
                Dispose();
            }
        }
          
        public ActionResult PaytmResponse()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string paytmChecksum = "";
            foreach (string key in Request.Form.Keys)
            {
                parameters.Add(key.Trim(), Request.Form[key]);
            }

            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                paytmChecksum = parameters["CHECKSUMHASH"];
                parameters.Remove("CHECKSUMHASH");
            }

            if (parameters.ContainsKey("TXNID"))
            {
                transid = parameters["TXNID"];
                status = parameters["STATUS"];
                respcode = parameters["RESPCODE"];
            }

            if (status == "TXN_SUCCESS" && respcode == "01")
            {
                if (globalreqfrom == "Admission")
                {
                    //UpdateTable(globalreqfrom, transid);
                    return RedirectToAction("AdmissionSuccess");
                }
                else
                {
                    return RedirectToAction("Success", "Payment");
                }

            }
            else
            {
                if (globalreqfrom == "Admission")
                {
                    return RedirectToAction("AdmissionFailure");
                }
                else
                {
                    return RedirectToAction("Cancel", "Payment");
                }
            }
        }

        #region Helpers

        private void SetEnvironmentValues()
        {
            string env = config.GetValue<string>("paytm:string");
            if (env == "Devlopment")
            {
                MID = "szIlUC39812499765695";
                Mkey = "vT4MhrO9Gl&XCea9";
                PaytmURL = "https://securegw-stage.paytm.in/theia/api/v1/initiateTransaction?mid=szIlUC39812499765695&orderId=";
                CallbackUrl = "https://localhost:44345/Payment/PaytmResponse";
            }
            else
            {
                MID = "djoqSC77303259399208";
                Mkey = "PzpeSGT7QxQ4TfMo";
                PaytmURL = "https://securegw.paytm.in/theia/api/v1/initiateTransaction?mid=djoqSC77303259399208&orderId=";
                CallbackUrl = "http://www.brainfieldindia.in/Payment/PaytmResponse";
            }

        }

        private void SendRequest()
        {
            string post_data = JsonConvert.SerializeObject(RequestBody);

            string url = PaytmURL + OrderId;

            webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";
            webRequest.ContentLength = post_data.Length;

            using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
            {
                requestWriter.Write(post_data);
            }
        }

        private void GetRequestResponse()
        {
            string responseData = string.Empty;

            using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
            {
                responseData = responseReader.ReadToEnd();

                MyDeserializedClass = JsonConvert.DeserializeObject<Root>(responseData);
                MyDeserializedClass.OrderId = OrderId;
                MyDeserializedClass.Amount = GlobalVariables.GrandTotal.ToString();
                Console.WriteLine(responseData);
            }
        }

        private string GenrateOrderNo()
        {
            return OrderId = Convert.ToString(DateTime.Now.Ticks);
        }

        private Dictionary<string, object> GenerateParameters()
        {
            string env = config.GetValue<string>("paytm:string");
            string MID = config.GetValue<string>("paytm:MID");

            TxnAmount.Add("value", GlobalVariables.GrandTotal.ToString());
            TxnAmount.Add("currency", "INR");

            UserInfo.Add("custId", UserId.ToString());

            Body.Add("requestType", "Payment");
            Body.Add("mid", MID);
            Body.Add("websiteName", "WEBSTAGING");
            Body.Add("orderId", OrderId);
            Body.Add("txnAmount", TxnAmount);
            Body.Add("userInfo", UserInfo);
            Body.Add("callbackUrl", CallbackUrl);

            return Body;
        }
        #endregion Helpers
    }
}
