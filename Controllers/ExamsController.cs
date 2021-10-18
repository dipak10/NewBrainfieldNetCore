using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Viewmodels;
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

        public AddToCartViewModel AddToCartViewModel { get; set; }

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
        [Route("Exam/{id}")]
        public async Task<IActionResult> SingleProduct(int id)
        {
            if (id > 0)
            {
                try
                {
                    var getProduct = await (from x in entity.tblExamMaster
                                            join y in entity.tblExamCategory
                                            on x.ExamCategoryID equals y.ExamCategoryID
                                            join z in entity.tblExamSubject
                                            on x.ExamID equals z.ExamID
                                            join a in entity.tblSubject
                                            on z.SubjectID equals a.SubjectID
                                            select new SingleProductViewModel
                                            {
                                                SingleExamViewModel = new SingleExamViewModel
                                                {
                                                    ExamID = x.ExamID,
                                                    ExamName = x.ExamName,
                                                    CategoryName = y.ExamCategoryName,
                                                    DifficultyLevel = "Easy, Medium, Hard",
                                                    Description = x.Description,
                                                    Duration = x.TimeDuration,
                                                    SellPrice = x.SellPrice,
                                                    Students = 2000,
                                                    Subject = a.SubjectName,
                                                    ImageName = x.ImageName,
                                                },
                                                AddToCartViewModel = new Viewmodels.AddToCartViewModel
                                                {
                                                    ProductID = x.ExamID,
                                                    Type = "Exam"
                                                }
                                            }).Where(x => x.SingleExamViewModel.ExamID == id).FirstOrDefaultAsync();

                    if (getProduct == null) return RedirectToAction("Index", "Home");

                    return View(getProduct);
                }
                catch (Exception e)
                {
                    notyf.Error("Some error occured");
                }
                finally
                {
                    Dispose();
                }
            }
            return RedirectToAction("Index", "Home");
        }        
    }
}
