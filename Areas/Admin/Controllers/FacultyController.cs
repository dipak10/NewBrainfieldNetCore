using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacultyController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper mapper;

        public FacultyController(ApplicationContext entity, INotyfService notyf, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            this.entity = entity;
            this.notyf = notyf;
            this._hostEnvironment = hostEnvironment;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<tblFaculties> faculties = await entity.tblFaculties.ToListAsync();
            

            return View(faculties);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(FacultiesDTO model)
        {
            if (ModelState.IsValid)
            {
                tblFaculties faculty = new tblFaculties();
                faculty.FacultyName = model.FacultyName;
                faculty.SubjectName = model.SubjectName;
                faculty.Experience = model.Experience;
                faculty.Details = model.Details;
                faculty.Photo = await UploadedFile(model);
                entity.tblFaculties.Add(faculty);
                await entity.SaveChangesAsync();

                ModelState.Clear();

                notyf.Success("Faculty Added Successfully");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id > 0)
            {
                var data = entity.tblFaculties.Where(x => x.FacultyID == id).FirstOrDefault();
                if (data != null)
                {
                    entity.tblFaculties.Remove(data);
                    entity.SaveChanges();
                    notyf.Success("Faculty Deleted Successfully");
                }
            }
            return RedirectToAction("Index");
        }


        #region AddImage
        private async Task<string> UploadedFile(FacultiesDTO model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images\\Faculty");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region RemoveImage

        private bool RemoveImage(string name)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images\\Faculty", name);
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
