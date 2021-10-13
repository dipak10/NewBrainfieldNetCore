using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Areas.Admin.Models;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudyVideosController : Controller
    {
        private readonly IStudyVideosServices _studyVideosServices;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
       
        public StudyVideosController(IStudyVideosServices studyVideosServices, IMapper mapper, INotyfService notyf)
        {
            _studyVideosServices = studyVideosServices;
            this._mapper = mapper;
            this._notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var videos = await _studyVideosServices.GetVideos();
            var model = new ListStudyVideosViewModel()
            {
                Videos = videos
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddStudyVideosViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var map = _mapper.Map<tblVideos>(model);
                    var call = await _studyVideosServices.AddVideoes(map);
                    ModelState.Clear();
                    _notyf.Success("Video Added Successfully");
                    return View();
                }
                catch (Exception e)
                {
                    _notyf.Error("Error Occured Something went wrong");
                    ModelState.AddModelError("Error", e.Message);
                }
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                _studyVideosServices.DeleteVideo(id);
                _notyf.Success("Video Deleted Successfully");
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> MakeFreeForUsers()
        {
            return null;
            //var data = await _studyVideosServices.GetUsers();
            //return View(data);
        }

        public async Task<ActionResult> ChangeExcemption(int? id)
        {
            if (id > 0)
            {
                bool success = await _studyVideosServices.ChangeExcemption(id.Value);
                return RedirectToAction("MakeFreeForUsers");
            }
            return RedirectToAction("MakeFreeForUsers");
        }
    }
}
