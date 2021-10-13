using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GalleryController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GalleryController(ApplicationContext entity, INotyfService notyf, IWebHostEnvironment hostEnvironment)
        {
            this._entity = entity;
            this._notyf = notyf;
            this._hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<tblGallery> galleries = await _entity.tblGallery.ToListAsync();
            return View(galleries);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(GalleryDTO dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var image in dto.Images)
                    {
                        string newImageName = string.Empty;

                        newImageName = await UploadedFile(image);

                        tblGallery gallery = new tblGallery();
                        gallery.ImageName = newImageName;
                        await _entity.tblGallery.AddAsync(gallery);
                        await _entity.SaveChangesAsync();

                        _notyf.Success($"Image {image.FileName} Upload Successfully");

                        ModelState.Clear();
                    }
                }
                catch (Exception e)
                {
                    _notyf.Error("Some error Occured while uploading");
                }
                return View();
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                var data = await _entity.tblGallery.Where(x => x.GalleryID == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    RemoveImage(data.ImageName);

                    _entity.tblGallery.Remove(data);
                    await _entity.SaveChangesAsync();
                    _notyf.Success("Image Deleted Successfully");
                    return View();
                }
            }
            _notyf.Error("Error Occured while deleting");
            return View();
        }

        #region AddImage
        private async Task<string> UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file.Length > 0)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images\\Gallery");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region RemoveImage

        private bool RemoveImage(string name)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images\\Gallery", name);
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
