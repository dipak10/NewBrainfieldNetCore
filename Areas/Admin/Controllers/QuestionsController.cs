using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewBrainfieldNetCore.Areas.Admin.Models;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionsController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly ICommonService commonService;
        private readonly INotyfService notyf;

        public QuestionsController(ApplicationContext entity, ICommonService commonService, INotyfService notyf)
        {
            this.entity = entity;
            this.commonService = commonService;
            this.notyf = notyf;
        }

        #region Helper
        public async Task<JsonResult> GetChapters(int id)
        {
            var cname = await commonService.GetChapters();
            var chapters = cname.Where(x => x.ChapterID == id).Select(x => new ChapterDTO { ChapterID = x.ChapterID, ChapterName = x.ChapterName }).ToList();
            return Json(chapters);
        }

        public async Task<List<SelectListItem>> GetExams()
        {
            var examMasters = await commonService.GetExams();
            List<SelectListItem> Exams = new List<SelectListItem>();
            foreach (var x in examMasters)
            {
                Exams.Add(new SelectListItem { Value = Convert.ToString(x.ExamID), Text = x.ExamName });
            }
            return Exams;
        }

        #endregion Helper

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var Exams = await GetExams();

            var standards = await commonService.GetStandards();

            var subjects = await commonService.GetSubjects();

            var chapters = await commonService.GetChapters();

            var model = new QuestionMasterModel()
            {
                ExamMasters = Exams,
                Standards = standards,
                Subjects = subjects,
                Chapters = chapters
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(QuestionMasterModel model)
        {
            try
            {
                tblQuestionMaster qm = new tblQuestionMaster();
                qm.StandardID = model.StandardID;
                qm.SubjectID = model.SubjectID;
                qm.ChapterID = model.ChapterID;
                qm.Question = model.Question;
                qm.Type = "Objective";
                qm.Mark = model.Mark;
                qm.NegativeMark = model.NegativeMark;
                qm.CreatedOn = System.DateTime.Now.ConvertToIndianTime();
                entity.tblQuestionMaster.Add(qm);
                entity.SaveChanges();

                var getdata = entity.tblQuestionMaster.Select(x => x.QuestionMasterID).Max();

                tblExamQuestion eq = new tblExamQuestion();
                eq.ExamID = model.ExamID;
                eq.QuestionID = getdata;
                eq.CreatedOn = System.DateTime.Now.ConvertToIndianTime();
                entity.tblExamQuestion.Add(eq);
                entity.SaveChanges();

                tblQuestionOptionMaster qo = new tblQuestionOptionMaster();
                qo.QuestionID = getdata;
                qo.OptionA = model.OptionA;
                qo.OptionB = model.OptionB;
                qo.OptionC = model.OptionC;
                qo.OptionD = model.OptionD;
                qo.Explanation = model.Explanation;
                qo.CorrectAnswer = model.CorrectAnswer;
                entity.tblQuestionOptionMaster.Add(qo);
                entity.SaveChanges();

                ModelState.Clear();

                notyf.Success("Question added successfully");
            }
            catch (Exception e)
            {
                notyf.Error("Error occured while adding questions");
            }
            return RedirectToAction("Add");
        }

    }
}
