using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ChapterController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly ICommonService _commonService;
        private readonly INotyfService _notyf;

        ChapterDTO chapterDTO = new ChapterDTO();

        public ChapterController(ApplicationContext entity, ICommonService commonService,
            INotyfService notyf)
        {
            _entity = entity;
            _commonService = commonService;
            _notyf = notyf;
        }

        public async Task<JsonResult> GetSubject(int id)
        {
            var cname = await _commonService.GetSubjects();
            var subjects = cname.Where(x => x.StandardID == id).Select(x => new SubjectDTO { SubjectID = x.SubjectID, SubjectName = x.SubjectName }).ToList();
            return Json(subjects);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<tblChapters> chapters = await _commonService.GetChapters();          
            return View(chapters);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var std = await _commonService.GetStandards();
            var sub = await _commonService.GetSubjects();
            chapterDTO.Standards = std;
            chapterDTO.Subjects = sub;
            return View(chapterDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChapterDTO model)
        {
            if (ModelState.IsValid)
            {
                tblChapters chapters = new tblChapters();
                chapters.SubjectID = model.SubjectID;
                chapters.ChapterName = model.ChapterName;
                await _entity.tblChapters.AddAsync(chapters);
                await _entity.SaveChangesAsync();
                ModelState.Clear();
                _notyf.Success("Chapter Added Successfully");
                return RedirectToAction("Add");
            }
            return RedirectToAction("Add");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _entity.tblChapters.FirstOrDefaultAsync(x => x.ChapterID == id);
            if(data != null)
            {
                _entity.tblChapters.Remove(data);
                await _entity.SaveChangesAsync();
                _notyf.Success("Chapter deleted successfully");
            }
            return RedirectToAction("Index");
        }
    }
}
