using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DownloadsController : Controller
    {
        private ApplicationContext _entity;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _hostEnvironment;

        DownloadsDTO downloadsDTO = new DownloadsDTO();

        public DownloadsController(ApplicationContext entity,
            IMapper mapper,
            INotyfService notyf,
            IWebHostEnvironment hostEnvironment)
        {
            this._entity = entity;
            this._mapper = mapper;
            this._notyf = notyf;
            this._hostEnvironment = hostEnvironment;
        }


        public IActionResult Index()
        {
            var data = (from x in _entity.tblDownloads
                        join y in _entity.tblDownloadCategory on x.Section equals y.DownloadCategoryID
                        select new DownloadsDTO
                        {
                            DownloadsID = x.DownloadsID,
                            DisplayFileName = x.FileName,
                            Title = x.Title,
                            IsFree = x.IsFree,
                            CategoryName = y.DownloadCategoryName,
                            UploadOn = x.UploadOn
                        }).ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Add()
        {
            downloadsDTO.DownloadCategories = GetDownloadCategories();

            return View(downloadsDTO);
        }

        [HttpPost]
        public ActionResult Add(DownloadsDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var map = _mapper.Map<tblDownloads>(model);
                    string newFileName = UploadedFile(model);
                    map.FileName = newFileName;
                    map.UploadOn = DateTime.Now.ConvertToIndianTime();
                    _entity.tblDownloads.Add(map);
                    _entity.SaveChanges();

                    _notyf.Success("Download File Added Successfully");

                    ModelState.Clear();

                    downloadsDTO.DownloadCategories = GetDownloadCategories();

                    return View(downloadsDTO);
                }

                downloadsDTO.DownloadCategories = GetDownloadCategories();

                return View(downloadsDTO);
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
                    var data = _entity.tblDownloads.Where(x => x.DownloadsID == id).FirstOrDefault();
                    if (data != null)
                    {
                        RemoveFile(data.FileName);

                        _entity.tblDownloads.Remove(data);
                        _entity.SaveChanges();

                        _notyf.Success("Downloads File Deleted Successfully");

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



        private List<tblDownloadCategory> GetDownloadCategories()
        {
            return _entity.tblDownloadCategory.ToList();
        }

        #region AddImage
        private string UploadedFile(DownloadsDTO model)
        {
            string uniqueFileName = null;

            if (model.FileName != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "DownloadFiles");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FileName.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region RemoveImage

        private bool RemoveFile(string name)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "DownloadFiles", name);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return true;
            }
            return false;
        }

        #endregion RemoveImage
    }
}
