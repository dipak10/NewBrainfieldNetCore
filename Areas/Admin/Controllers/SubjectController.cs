using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubjectController : Controller
    {
        private ISubjectService _subjectService;
        private ICommonService _commonService;
        private readonly INotyfService _notyf;

        SubjectDTO subjectDTO = new SubjectDTO { };

        public SubjectController(ISubjectService subjectService, ICommonService commonService, INotyfService notyf)
        {
            _subjectService = subjectService;
            _commonService = commonService;
            _notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _commonService.GetSubjects();                    
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            await GetData();

            return View(subjectDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SubjectDTO subject)
        {
            if (ModelState.IsValid)
            {
                var subjectId = _subjectService.AddSubject(subject);
                if (subjectId > 0)
                {
                    ModelState.Clear();

                    _notyf.Success("Success");

                    await GetData();

                    return View(subjectDTO);
                }
                else
                {
                    await GetData();
                    return View(subjectDTO);
                }
            }
            return View(subjectDTO);
        }

        private async Task GetData()
        {
            List<StandardDTO> standards = await _commonService.GetStandards();

            subjectDTO.Standards = standards;
        }
    }
}
