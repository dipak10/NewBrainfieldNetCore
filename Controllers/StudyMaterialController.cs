using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class StudyMaterialController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;
        private readonly IEmailSender emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;

        public string OrderNo { get; set; }

        public StudyMaterialController(ApplicationContext entity, INotyfService notyf, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment)
        {
            this.entity = entity;
            this.notyf = notyf;
            this.emailSender = emailSender;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int id)
        {
            if (id > 0)
            {
                try
                {
                    List<StudentStudyMaterialCatwiseIndexViewModel> data = await (from x in entity.tblStudyMaterialFiles
                                                                                  join y in entity.tblStudyMaterialCategories on
                                                                                  x.StudyMaterialCategoryID equals y.StudyMaterialCategoryID
                                                                                  where x.StudyMaterialCategoryID == id
                                                                                  select new StudentStudyMaterialCatwiseIndexViewModel
                                                                                  {
                                                                                      StudyMaterialId = x.StudyMaterialFilesID,
                                                                                      Title = x.Title,
                                                                                      StudyCategoryName = y.StudyMaterialCategoryName,
                                                                                      Price = x.Price,
                                                                                      Description = x.Description,
                                                                                      ImageName = x.ImageName
                                                                                  }).ToListAsync();

                    return View(data);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    Dispose();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Checkout(int id)
        {
            GlobalVariables.StudyMaterialId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(StudyMaterialCheckoutViewModel model)
        {
            GlobalVariables.PaymentRequestFrom = PaymentRequestFrom.StudyMaterial;

            var getpayment = entity.tblStudyMaterialFiles.Where(x => x.StudyMaterialFilesID == GlobalVariables.StudyMaterialId).FirstOrDefault();
            if (getpayment != null)
            {
                GlobalVariables.GrandTotal = getpayment.Price;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tblStudyMaterialPayment smp = new tblStudyMaterialPayment();
                    smp.Name = model.BuyerName;
                    smp.EmailID = model.BuyerEmail;
                    smp.PhoneNumber = model.BuyerPhone;
                    smp.StudyMaterialFileID = GlobalVariables.StudyMaterialId;
                    smp.OrderNo = GeneratateOrderNo();
                    smp.IsPaid = false;
                    smp.IsDownloaded = false;
                    smp.CreatedOn = DateTime.Now.ConvertToIndianTime();
                    await entity.tblStudyMaterialPayment.AddAsync(smp);
                    await entity.SaveChangesAsync();
                    ModelState.Clear();
                    //notyf.Success("Data Saved, Redirecting to Payment");
                    return RedirectToAction("ProcessPayment", "Payment");
                }
                catch (Exception e)
                {
                    notyf.Error("Data not saved, try again");
                }
                finally
                {
                    Dispose();
                }
            }
            return View();
        }

        public async Task<IActionResult> StudyMaterialPaymentSuccess()
        {
            try
            {
                var data = entity.tblStudyMaterialPayment.Where(x => x.OrderNo == Convert.ToString(GlobalVariables.OrderId)).FirstOrDefault();
                if (data != null)
                {
                    data.IsPaid = true;
                    entity.SaveChanges();
                    //string url = "http://brainfieldindia.in/StudyMaterial/Download?email=" + data.EmailID + "&orderno=" + OrderNo;
                    string url = "https://localhost:44345/StudyMaterial/Download?email=" + data.EmailID + "&orderno=" + GlobalVariables.OrderId;
                    string content = "Hello,";
                    content += "Click below link to download your file";
                    content += "<a href='" + url + "'> <u> Click Here to download file </u> </a>";

                    var request = new MailRequest("dipak10494@yahoo.com", "Brainfield File Download Link", content);

                    await emailSender.SendEmailAsync(request);

                    notyf.Success("File link via email Sent Successfully");

                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home");
            }
            finally
            {
                Dispose();
            }
        }

        public async Task<IActionResult> Download(string email, string orderno)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(orderno))
            {
                try
                {
                    var getfile = entity.tblStudyMaterialPayment.Where(x => x.OrderNo == orderno && x.IsDownloaded == false).FirstOrDefault();
                    if (getfile != null)
                    {
                        var studyfile = entity.tblStudyMaterialFiles.Where(x => x.StudyMaterialFilesID == getfile.StudyMaterialFileID).FirstOrDefault();

                        if (studyfile != null)
                        {
                            getfile.IsDownloaded = false;
                            getfile.DownloadedOn = DateTime.Now;
                            await entity.SaveChangesAsync();

                            string webRootPath = webHostEnvironment.WebRootPath;
                            
                            string path = "";
                            path = Path.Combine(webRootPath, "StudyMaterialFiles");

                            string fileName = studyfile.FileName;
                            
                            path = path + "\\" + fileName;

                            return PhysicalFile(path, "application/zip", fileName);
                        }
                        else
                        {
                            notyf.Error($"File not found");
                        }
                    }
                    else
                    {
                        notyf.Warning($"Either your order is not exist or you have already downloaded your file");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error Occured {e.InnerException} + {e.Message}");
                }
            }
            Dispose();
            return RedirectToAction("Index", "Home");
        }

        private string GeneratateOrderNo()
        {
            OrderNo = Convert.ToString(DateTime.Now.Ticks);
            GlobalVariables.OrderId = Convert.ToInt64(OrderNo);
            return OrderNo;
        }

    }
}
