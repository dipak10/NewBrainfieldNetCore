using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Areas.Admin.Models;
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
    public class StudyMaterialFilesController : Controller, IDisposable
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper mapper;

        AdminStudyMaterialIndexViewModel viewModel = new AdminStudyMaterialIndexViewModel();

        public StudyMaterialFilesController(ApplicationContext entity, INotyfService notyf, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            this.entity = entity;
            this.notyf = notyf;
            this._hostEnvironment = hostEnvironment;
            this.mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            List<AdminStudyMaterialIndexViewModel> data = await (from x in entity.tblStudyMaterialFiles
                                                                 join y in entity.tblStudyMaterialCategories on
                                                                 x.StudyMaterialCategoryID equals y.StudyMaterialCategoryID
                                                                 select new AdminStudyMaterialIndexViewModel
                                                                 {
                                                                     StudyMaterialId = x.StudyMaterialFilesID,
                                                                     StudyMaterialCategoryID = x.StudyMaterialCategoryID,
                                                                     Title = x.Title,
                                                                     StudyCategoryName = y.StudyMaterialCategoryName,
                                                                     Price = x.Price,
                                                                     Description = x.Description,
                                                                     DisplayFileName = x.FileName
                                                                 }).ToListAsync();
            Dispose(true);
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<tblStudyMaterialCategories> categories = await GetTblStudyMaterialCategories();

            viewModel.StudyMaterialCategories = categories;

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdminStudyMaterialIndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UploadFileName = UploadedFile(model);
                    var UploadImage = UploadedImage(model);

                    await Task.WhenAll(UploadFileName, UploadImage);

                    tblStudyMaterialFiles studyMaterialFile = new tblStudyMaterialFiles();
                    studyMaterialFile.StudyMaterialCategoryID = model.StudyMaterialCategoryID;
                    studyMaterialFile.Title = model.Title;
                    studyMaterialFile.Description = model.Description;
                    studyMaterialFile.Price = model.Price;
                    studyMaterialFile.FileName = UploadFileName.Result;
                    studyMaterialFile.ImageName = UploadImage.Result;
                    studyMaterialFile.CreatedOn = DateTime.Now.ConvertToIndianTime();
                    entity.tblStudyMaterialFiles.Add(studyMaterialFile);
                    await entity.SaveChangesAsync();
                    notyf.Success("File Upload Successfully");
                    ModelState.Clear();

                    viewModel.StudyMaterialCategories = await GetTblStudyMaterialCategories();

                    return View(viewModel);
                }
                catch (Exception e)
                {
                    notyf.Error("Some thing went wrong try again");
                    ModelState.AddModelError("error", e.Message);
                }
                Dispose(true);
            }

            viewModel.StudyMaterialCategories = await GetTblStudyMaterialCategories();

            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await entity.tblStudyMaterialFiles.Where(x => x.StudyMaterialFilesID == id).FirstOrDefaultAsync();
            if (data != null)
            {
                var removeFile = RemoveFile(data.FileName);
                var removeImage = RemoveImage(data.ImageName);

                entity.tblStudyMaterialFiles.Remove(data);
                await entity.SaveChangesAsync();

                notyf.Success("Delete Successfully");
                return RedirectToAction("Index");
            }
            notyf.Error("Error Occured During Delete");
            return RedirectToAction("Index");
        }

        #region AddFilesandImage
        private async Task<string> UploadedImage(AdminStudyMaterialIndexViewModel model)
        {
            string uniqueFileName = null;

            if (model.UplodCoverPic != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images\\StudyMaterialFileCoverPic");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.UplodCoverPic.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.UplodCoverPic.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        private async Task<string> UploadedFile(AdminStudyMaterialIndexViewModel model)
        {
            string uniqueFileName = null;

            if (model.UploadFileName != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "StudyMaterialFiles");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.UploadFileName.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.UploadFileName.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }

        #endregion

        #region RemoveFilesAndImage

        private async Task<bool> RemoveImage(string name)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images\\StudyMaterialFileCoverPic", name);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return true;
            }
            return false;
        }

        private async Task<bool> RemoveFile(string name)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "StudyMaterialFiles", name);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
                return true;
            }
            return false;
        }

        #endregion RemoveImage

        private async Task<List<tblStudyMaterialCategories>> GetTblStudyMaterialCategories()
        {
            return await entity.tblStudyMaterialCategories.ToListAsync();
        }

        protected new void Dispose()
        {
            entity.Dispose();
            base.Dispose(true);
            GC.Collect();
            Dispose();
        }
    }
}
