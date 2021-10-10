using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StandardController : Controller
    {
        private IStandardService _standardService;
        private ICommonService _commonService;

        private readonly INotyfService _notyf;

        public StandardController(IStandardService standardService, ICommonService commonService,
            INotyfService notyf)
        {
            _standardService = standardService;
            _commonService = commonService;
            _notyf = notyf;
        }        


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StandardDTO> standards = await _commonService.GetStandards();
            return View(standards);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(StandardDTO standardDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var standard = _standardService.AddStandard(standardDTO);
                    if (standard > 0)
                    {
                        _notyf.Success("Standard Added Successfully!");
                        ModelState.Clear();
                    }
                    _notyf.Error("Something went wrong try again");
                }
                catch (Exception e)
                {
                    _notyf.Error("Something went wrong try again");
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id > 0)
            {
                bool call = await _standardService.DeleteStandard(id.Value);
                if (call)
                {
                    _notyf.Success("Deleted Successs");
                }
                else
                {
                    _notyf.Error("Error while deleting");
                }
            }
            else
            {
                _notyf.Error("Error while deleting");
            }
            return RedirectToAction("Index");
        }
    }   
}
