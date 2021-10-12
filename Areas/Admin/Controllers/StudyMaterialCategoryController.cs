using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudyMaterialCategoryController : Controller
    {

        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper mapper;

        public StudyMaterialCategoryController(ApplicationContext entity, INotyfService notyf, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            this.entity = entity;
            this.notyf = notyf;
            this._hostEnvironment = hostEnvironment;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            List<tblStudyMaterialCategories> list = entity.tblStudyMaterialCategories.ToList();
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(StudyMaterialCategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblStudyMaterialCategories studyCategory = new tblStudyMaterialCategories();
                    studyCategory.StudyMaterialCategoryName = model.StudyMaterialCategoryName.Trim();
                    entity.tblStudyMaterialCategories.Add(studyCategory);
                    entity.SaveChanges();
                    ModelState.Clear();
                    notyf.Success("Category Deleted Successfully");
                }
                catch (Exception e)
                {
                }
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id > 0)
            {
                var data = entity.tblStudyMaterialCategories.Where(x => x.StudyMaterialCategoryID == id).FirstOrDefault();
                if (data != null)
                {
                    entity.tblStudyMaterialCategories.Remove(data);
                    entity.SaveChanges();
                    notyf.Success("Category Deleted Successfully");
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
