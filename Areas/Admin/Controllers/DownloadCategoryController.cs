using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DownloadCategoryController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;

        public DownloadCategoryController(ApplicationContext entity, IMapper mapper, INotyfService notyf)
        {
            this._entity = entity;
            this._mapper = mapper;
            this._notyf = notyf;
        }

        public IActionResult Index()
        {
            List<tblDownloadCategory> model = _entity.tblDownloadCategory.ToList();            

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(DownloadCategoryDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var map = _mapper.Map<tblDownloadCategory>(model);

                    _entity.tblDownloadCategory.Add(map);
                    _entity.SaveChanges();

                    _notyf.Success("Category Added Successfully");

                    ModelState.Clear();

                    return View();
                }
                return View();
            }
            catch
            {
                return View(model);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                if (id > 0)
                {
                    var data = _entity.tblDownloadCategory.Where(x => x.DownloadCategoryID == id).FirstOrDefault();
                    if (data != null)
                    {
                        _entity.tblDownloadCategory.Remove(data);
                        _entity.SaveChanges();

                        _notyf.Success("Category Deleted Successfully");

                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }
        }
    }
}
