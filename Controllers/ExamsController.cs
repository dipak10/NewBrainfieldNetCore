using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels.StudentExamViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    [Route("Student/Products/")]
    [Route("Student/Products/Category/")]
    public class ExamsController : Controller
    {
        private readonly ApplicationContext entity;

        private readonly INotyfService notyf;

        public ExamsController(ApplicationContext entity, INotyfService notyf)
        {
            this.entity = entity;
            this.notyf = notyf;
        }

        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> Index(string name)
        {
            var getId = await entity.tblExamCategory.Where(x => x.ExamCategoryName == name).FirstOrDefaultAsync();

            if (getId == null) return RedirectToAction("Index", "Home");

            List<tblExamMaster> data = await entity.tblExamMaster.Where(x => x.ExamCategoryID == getId.ExamCategoryID).ToListAsync();
            if (data == null) return RedirectToAction("Index", "Home");

            var model = new CategorywiseExamListViewModel()
            {
                Exams = data,
                CategoryName = getId.ExamCategoryName
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();

        }
    }
}
