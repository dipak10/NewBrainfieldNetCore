using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class StudyMaterialController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;

        public StudyMaterialController(ApplicationContext entity, INotyfService notyf)
        {
            this.entity = entity;
            this.notyf = notyf;
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
                catch(Exception e)
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
                    notyf.Success("Data Saved, Redirecting to Payment");
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

        private string GeneratateOrderNo()
        {
            return DateTime.Now.Ticks.ToString();
        }
    }   
}
