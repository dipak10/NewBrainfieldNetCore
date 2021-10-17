using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Areas.Admin.Models;
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
    public class PackagesController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly ICommonService _commonService;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _hostEnvironment;

        public int PackageMaterId { get; set; }

        public PackagesController(ApplicationContext entity, ICommonService commonService,
            INotyfService notyf, IWebHostEnvironment hostEnvironment)
        {
            _entity = entity;
            _commonService = commonService;
            _notyf = notyf;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<tblPackageMaster> packageMasters = await _entity.tblPackageMaster.ToListAsync();
                return View(packageMasters);
            }
            catch (Exception)
            {
                return RedirectToAction("Add");
            }
            finally
            {
                Dispose();
            }
        }

        // GET: AdminPackages/Add
        public async Task<IActionResult> Add()
        {
            try
            {
                IEnumerable<tblExamMaster> examMasters = await _entity.tblExamMaster.ToListAsync();

                List<SelectListItem> Exams = new List<SelectListItem>();
                foreach (var x in examMasters)
                {
                    Exams.Add(new SelectListItem { Value = Convert.ToString(x.ExamID), Text = x.ExamName });
                }

                var model = new AddPackageViewModel()
                {
                    ExamMasters = Exams
                };
                return View(model);
            }
            catch (Exception e)
            {
                return View();
            }
            finally
            {
                Dispose();
            }
        }

        // POST: AdminPackages/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddPackageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uploadImage = await UploadImage(model);

                    tblPackageMaster packageMaster = new tblPackageMaster();
                    packageMaster.PackageName = model.PackageName;
                    packageMaster.SellPrice = model.SellPrice;
                    packageMaster.Description = model.Description;
                    packageMaster.Active = true;
                    packageMaster.ImageName = uploadImage;
                    packageMaster.CreatedOn = DateTime.Now.ConvertToIndianTime();
                    _entity.tblPackageMaster.Add(packageMaster);
                    await _entity.SaveChangesAsync();

                    PackageMaterId = packageMaster.PackageMasterID;

                    await AddToPackageExam(model);

                    _notyf.Success("Package Added Successfully");

                    ModelState.Clear();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
                finally
                {
                    Dispose();
                }
            }
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    await DeletePackageExams(id);

                    tblPackageMaster packageMaster = await _entity.tblPackageMaster.Where(x => x.PackageMasterID == id).FirstAsync();
                    RemoveImage(packageMaster.ImageName);
                    _entity.tblPackageMaster.Remove(packageMaster);
                    await _entity.SaveChangesAsync();

                    _notyf.Success("Package Deleted Successfully");

                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    _notyf.Error("Package Not Deleted");
                    return RedirectToAction("Index");
                }
                finally
                {
                    Dispose();
                }
            }
            return View();
        }

        #region Helper

        [HttpPost]
        public async Task<JsonResult> GetPrice(int[] id)
        {
            string price = string.Empty;
            decimal sum = 0;
            foreach (var r in id)
            {
                tblExamMaster examMasters = await _entity.tblExamMaster.Where(x => x.ExamID == r).FirstAsync();
                sum += examMasters.SellPrice;
            }
            price = Convert.ToString(sum);

            return Json(price);
        }

        private async Task<bool> AddToPackageExam(AddPackageViewModel model)
        {
            if (PackageMaterId > 0)
            {

                foreach (var r in model.SelectedExams)
                {
                    tblPackageExam packageExam = new tblPackageExam();
                    packageExam.PackageID = PackageMaterId;
                    packageExam.ExamID = Convert.ToInt32(r);
                    packageExam.AddedOn = DateTime.Now;
                    await _entity.tblPackageExam.AddAsync(packageExam);
                    await _entity.SaveChangesAsync();
                }
                return true;
            }
            return false;
        }

        private async Task<bool> DeletePackageExams(int id)
        {
            IEnumerable<tblPackageExam> PackageExams = await _entity.tblPackageExam.Where(x => x.PackageID == id).ToListAsync();
            _entity.tblPackageExam.RemoveRange(PackageExams);
            await _entity.SaveChangesAsync();
            return true;
        }

        #endregion Helper

        #region AddImage
        private async Task<string> UploadImage(AddPackageViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "Images\\PackageCoverPic");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region RemoveImage

        private bool RemoveImage(string name)
        {
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images\\PackageCoverPic", name);
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
