using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ExamController : Controller
    {
        private readonly ICommonService commonService;
        private readonly INotyfService notyf;
        private ApplicationContext applicationContext;
        private readonly IMapper mapper;

        ProductsDTO ProductsDTO = new ProductsDTO();

        public ExamController(ICommonService commonService,
            INotyfService notyf, ApplicationContext applicationContext, IMapper mapper)
        {
            this.commonService = commonService;
            this.notyf = notyf;
            this.applicationContext = applicationContext;
            this.mapper = mapper;
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
        public async Task<IActionResult> Add(ProductsDTO dto)
        {

        }


        private async Task InitData()
        {
            ProductsDTO.Standards = await commonService.GetStandards();
            ProductsDTO.Subjects = await commonService.GetSubjects();
            ProductsDTO.ExamCategories = await applicationContext.tblExamCategory.ToListAsync();
        }
    }
}
