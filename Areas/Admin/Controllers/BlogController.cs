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
    public class BlogController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly ICommonService _commonService;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BlogController(ApplicationContext entity, ICommonService commonService, INotyfService notyf, IWebHostEnvironment hostEnvironment)
        {
            this._entity = entity;
            this._commonService = commonService;
            this._notyf = notyf;
            this._hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            List<tblBlogs> blogs = _entity.tblBlogs.ToList();
            return View(blogs);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(BlogDTO dto)
        {
            string image = UploadedFile(dto);

            tblBlogs blog = new tblBlogs();
            blog.BlogTitle = dto.BlogTitle;
            blog.BlogContent = dto.BlogContent;
            blog.BlogImage = image;
            blog.IsActive = true;
            blog.IsAppOnly = false;
            blog.CreatedDate = DateTime.Now.ConvertToIndianTime();

            _entity.tblBlogs.Add(blog);
            _entity.SaveChanges();

            ModelState.Clear();

            return View();
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (id > 0)
                {
                    tblBlogs data = _entity.tblBlogs.Where(x => x.BlogId == id).FirstOrDefault();
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
                return RedirectToAction("DisplayBlog");
            }
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                if (id > 0)
                {
                    var data = _entity.tblBlogs.Where(x => x.BlogId == id).First();
                    if (data != null)
                    {
                        RemoveImage(data.BlogImage);
                        _entity.tblBlogs.Remove(data);
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
        private string UploadedFile(BlogDTO model)
        {
            string uniqueFileName = null;

            if (model.BlogImage != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images\\Blog");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BlogImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BlogImage.CopyTo(fileStream);
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
