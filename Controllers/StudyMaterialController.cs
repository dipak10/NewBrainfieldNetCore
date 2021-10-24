using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
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
        public async Task<IActionResult> Checkout(Democlass model)
        {
            return View();
        }
    }


    public class Democlass
    {
        public string BuyerName { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }
    }
}
