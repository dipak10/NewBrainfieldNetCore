using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NewBrainfieldNetCore.Common.Requests;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Viewmodels.Cart;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IConfiguration config;
        private readonly ApplicationContext context;
        private readonly INotyfService notyf;

        private HttpWebRequest webRequest;
        private QueryHelper queryHelper;
        private string transid, status, respcode, globalreqfrom = string.Empty;

        public string OrderNo { get; set; }
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

        public PaymentController(IConfiguration config, ApplicationContext context, INotyfService notyf)
        {
            this.config = config;
            this.context = context;
            this.notyf = notyf;

            Body = new Dictionary<string, object>();
            Head = new Dictionary<string, string>();
            RequestBody = new Dictionary<string, object>();
            UserInfo = new Dictionary<string, string>();
            TxnAmount = new Dictionary<string, string>();

            MyDeserializedClass = new Root();

            queryHelper = new QueryHelper(applicationContext: context);

            if (GlobalVariables.OrderId > 0)
            {
                OrderNo = Convert.ToString(GlobalVariables.OrderId);
            }

            SetEnvironmentValues();
        }

        [HttpGet]
        public IActionResult ProcessPayment()
        {
            //GenrateOrderNo();

            Dictionary<string, object> body = GenerateParameters();

            string paytmChecksum = Paytm.Checksum.generateSignature(JsonConvert.SerializeObject(body), Mkey);

            Head.Add("signature", paytmChecksum);

            RequestBody.Add("body", body);
            RequestBody.Add("head", Head);

            SendRequest();

            GetRequestResponse();

            return View(MyDeserializedClass);
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
                Console.WriteLine($"Error Occured {e.InnerException}");
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
                if (GlobalVariables.PaymentRequestFrom == PaymentRequestFrom.Admission)
                {
                    //UpdateTable(globalreqfrom, transid);
                    return RedirectToAction("AdmissionSuccess");
                }
                else if (GlobalVariables.PaymentRequestFrom == PaymentRequestFrom.StudyMaterial)
                {
                    return RedirectToAction("StudyMaterialPaymentSuccess", "StudyMaterial");
                }
                else
                {
                    return RedirectToAction("Success", "Payment");
                }

            }
            else
            {
                if (GlobalVariables.PaymentRequestFrom  == PaymentRequestFrom.Admission)
                {
                    return RedirectToAction("AdmissionFailure");
                }
                else if (GlobalVariables.PaymentRequestFrom == PaymentRequestFrom.StudyMaterial)
                {
                    return RedirectToAction("PaymentFailure", "StudyMaterial");
                }
                else
                {
                    return RedirectToAction("Cancel", "Payment");
                }
            }
        }

        public async Task<IActionResult> Success()
        {
            try
            {
                GetUser();

                var cart = await queryHelper.GetCart();

                bool hasAddedToOrderTable = await AddToOrderTable(cart);
                if (hasAddedToOrderTable)
                {
                    bool isAddToExamAppear = await AddToExamAppear(cart);
                    if (isAddToExamAppear)
                    {
                        bool isRemove = await RemoveFromCart();
                        if (isRemove)
                        {
                            notyf.Success($"Orderd Placed Successfully {OrderNo}");

                            return View();
                        }
                    }
                }
                return View();

            }
            catch (Exception e)
            {
                notyf.Error($"Error occured while placing orders { OrderNo }");

                return RedirectToAction("Error", "Payment");
            }
            finally
            {
                Dispose();
            }
        }

        #region Helpers

        private async Task<bool> AddToOrderTable(IEnumerable<CartViewModel> cart)
        {
            try
            {
                tblOrderMaster od = new tblOrderMaster();
                foreach (var r in cart)
                {
                    od.UserID = UserId;
                    od.ExamID = r.ProductId;
                    od.OrderNo = Convert.ToString(GlobalVariables.OrderId);
                    od.Price = r.SellPrice;
                    od.PlacedOn = DateTime.Now.ConvertToIndianTime();
                    od.Type = r.Type;
                    await context.tblOrderMaster.AddAsync(od);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                Dispose();
            }
        }

        private async Task<bool> AddToExamAppear(IEnumerable<CartViewModel> carts)
        {
            try
            {
                tblExamAppear ea = new tblExamAppear();
                foreach (var r in carts)
                {
                    if (r.Type == "Exam")
                    {
                        ea.UserID = GlobalVariables.UserId;
                        ea.ExamID = r.ProductId;
                        int getorderid = await context.tblOrderMaster.Where(x => x.OrderNo == GlobalVariables.OrderId.ToString()).Select(x => x.OrderID).FirstAsync();
                        ea.OrderID = getorderid;
                        ea.Appear = 0;
                        ea.LastUpdated = DateTime.Now;
                        await context.tblExamAppear.AddAsync(ea);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        var getpackageexam = context.tblPackageExam.Where(x => x.PackageID == r.ProductId).ToList();
                        foreach (var d in getpackageexam)
                        {
                            ea.UserID = GlobalVariables.UserId;
                            ea.ExamID = d.ExamID;
                            int getorderid = context.tblOrderMaster.Where(x => x.OrderNo == GlobalVariables.OrderId.ToString()).Select(x => x.OrderID).First();
                            ea.OrderID = getorderid;
                            ea.Appear = 0;
                            ea.LastUpdated = DateTime.Now;
                            await context.tblExamAppear.AddAsync(ea);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        private async Task<bool> RemoveFromCart()
        {
            try
            {
                var cart = await context.tblUserCart.Where(x => x.UserID == UserId).ToListAsync();
                foreach (var x in cart)
                {
                    context.tblUserCart.Remove(x);
                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void GetUser()
        {
            UserId = GlobalVariables.UserId;
        }

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

            string url = PaytmURL + OrderNo;

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
                MyDeserializedClass.OrderId = OrderNo;
                MyDeserializedClass.Amount = GlobalVariables.GrandTotal.ToString();
                Console.WriteLine(responseData);
            }
        }

        private string GenrateOrderNo()
        {
            OrderNo = Convert.ToString(DateTime.Now.Ticks);
            GlobalVariables.OrderId = Convert.ToInt32(OrderNo);
            return OrderNo;
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
            Body.Add("orderId", OrderNo);
            Body.Add("txnAmount", TxnAmount);
            Body.Add("userInfo", UserInfo);
            Body.Add("callbackUrl", CallbackUrl);

            return Body;
        }
        #endregion Helpers
    }
}
