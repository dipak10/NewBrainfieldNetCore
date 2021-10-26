using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
    public class NewsController : Controller
    {
        private readonly ApplicationContext _entity;        
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _hostEnvironment;

        public NewsController(ApplicationContext entity, INotyfService notyf, IWebHostEnvironment hostEnvironment)
        {
            _entity = entity;            
            _notyf = notyf;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            List<tblNews> news = _entity.tblNews.ToList();
            return View(news);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(NewsDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string image = await UploadedFile(dto);

                    tblNews news = new tblNews();
                    news.NewsHeadline = dto.NewsHeadline;
                    news.NewsShortDetail = dto.NewsShortDetail;
                    news.NewsDetail = dto.NewsDetail;
                    news.ImageName = image;
                    news.IsActive = true;
                    news.CreatedDate = DateTime.Now.ConvertToIndianTime();

                    await _entity.tblNews.AddAsync(news);
                    await _entity.SaveChangesAsync();

                    _notyf.Success("News Added Successfully");

                    ModelState.Clear();

                    return View();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _notyf.Error("Error occured while adding news");
                }
                finally
                {
                    Dispose();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (id > 0)
                {
                    tblNews data = _entity.tblNews.Where(x => x.NewsID == id).FirstOrDefault();
                    if (data != null)
                    {
                        return View(data);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id > 0)
                {
                    var data = _entity.tblNews.Where(x => x.NewsID == id).FirstOrDefault();
                    if (data != null)
                    {
                        RemoveImage(data.ImageName);
                        _entity.tblNews.Remove(data);
                        _entity.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return RedirectToAction("Index");
        }

        #region AddImage
        private async Task<string> UploadedFile(NewsDTO model)
        {
            string uniqueFileName = null;

            if (model.ImageName != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images\\News");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageName.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region RemoveImage

        private bool RemoveImage(string name)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images\\Blog", name);
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
