using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
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
    [Area("Admin")]
    public class ExamCategoryController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly ICommonService _commonService;
        private readonly INotyfService _notyf;
        private readonly IMapper _mapper;

        public ExamCategoryController(ApplicationContext entity, ICommonService commonService,
            INotyfService notyf, IMapper mapper)
        {
            _entity = entity;
            _commonService = commonService;
            _notyf = notyf;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<tblExamCategory> examCategories = _entity.tblExamCategory.ToList();                        
            return View(examCategories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ExamCategoryDTO examCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var map = _mapper.Map<tblExamCategory>(examCategoryDTO);
                    _entity.tblExamCategory.Add(map);
                    await _entity.SaveChangesAsync();
                    _notyf.Success("Exam Category Added Successfully");
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _notyf.Error("Error while adding exam category");
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id > 0)
            {
                var data = await _entity.tblExamCategory.Where(x => x.ExamCategoryID == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    _entity.tblExamCategory.Remove(data);
                    await _entity.SaveChangesAsync();
                    _notyf.Success("Exam Category Deleted Successfully");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
