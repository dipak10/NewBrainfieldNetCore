using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
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

        public IActionResult Index()
        {
            return View();
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
    }
}
