using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Areas.Admin.Models;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Models;
using NewBrainfieldNetCore.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExamController : Controller
    {
        private readonly ICommonService commonService;
        private readonly INotyfService notyf;
        private ApplicationContext applicationContext;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hostEnvironment;

        ExamMasterDTO ProductsDTO = new ExamMasterDTO();

        public ExamController(ICommonService commonService,
            INotyfService notyf, ApplicationContext applicationContext, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            this.commonService = commonService;
            this.notyf = notyf;
            this.applicationContext = applicationContext;
            this.mapper = mapper;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<AdminExamsIndexViewModel> examdata = await (from x in applicationContext.tblExamMaster
                                                             join y in applicationContext.tblExamCategory on
                                                             x.ExamCategoryID equals y.ExamCategoryID
                                                             join z in applicationContext.tblExamSubject on
                                                             x.ExamID equals z.ExamID
                                                             join p in applicationContext.tblSubject on
                                                             z.SubjectID equals p.SubjectID
                                                             join q in applicationContext.tblStandard on
                                                             x.StandardID equals q.StandardID
                                                             select new AdminExamsIndexViewModel
                                                             {
                                                                 ExamID = x.ExamID,
                                                                 ExamName = x.ExamName,
                                                                 MarkPrice = x.MarkPrice,
                                                                 SellPrice = x.SellPrice,
                                                                 TimeDuration = x.TimeDuration,
                                                                 Repetation = x.Repetation,
                                                                 Notes = x.Notes,
                                                                 StandardName = q.StandardName,
                                                                 SubjectName = p.SubjectName,
                                                                 CategoryName = y.ExamCategoryName,
                                                                 IsFeatured = x.IsFeatured,
                                                                 CreatedOn = x.CreatedOn
                                                             }).ToListAsync();

            return View(examdata);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            await InitData();
            return View(ProductsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExamMasterDTO dto)
        {
            try
            {
                var map = mapper.Map<tblExamMaster>(dto);
                map.ImageName = await UploadedFile(dto);
                map.CreatedOn = DateTime.Now.ConvertToIndianTime();

                await applicationContext.tblExamMaster.AddAsync(map);
                await applicationContext.SaveChangesAsync();

                tblExamSubject examSubject = new tblExamSubject();
                examSubject.ExamID = map.ExamID;
                examSubject.SubjectID = dto.SubjectID;

                await applicationContext.tblExamSubject.AddAsync(examSubject);
                await applicationContext.SaveChangesAsync();
                notyf.Success("Exam Added Successfully");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.Message.ToString());
                notyf.Error("Error Occured");
            }
            return RedirectToAction("Index");
        }

        private async Task InitData()
        {
            ProductsDTO.Standards = await commonService.GetStandards();
            ProductsDTO.Subjects = await commonService.GetSubjects();
            ProductsDTO.ExamCategories = await applicationContext.tblExamCategory.ToListAsync();
        }

        private async Task<string> UploadedFile(ExamMasterDTO model)
        {
            string uniqueFileName = null;

            if (model.ImageName != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "Images\\ExamCoverPic");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageName.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task<IActionResult> Delete(int Id)
        {
            if (Id > 0)
            {
                var data = await applicationContext.tblExamMaster.FirstOrDefaultAsync(x => x.ExamID == Id);
                if (data != null)
                {
                    data.IsDeleted = true;
                    await applicationContext.SaveChangesAsync();
                    notyf.Success("Exam Deleted Successfully");
                    return RedirectToAction("Index");
                }
            }
            notyf.Error("Error Occured while deleting");
            return RedirectToAction("Index");
        }

    }
}
