using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TestimonialsController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;
        private readonly IWebHostEnvironment hostEnvironment;

        public TestimonialsController(ApplicationContext entity, INotyfService notyf, IWebHostEnvironment hostEnvironment)
        {
            this.entity = entity;
            this.notyf = notyf;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<tblTestimonials> testimonials = await entity.tblTestimonials.ToListAsync();
            return View(testimonials);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TestimonialDTO model)
        {
            string imageName = string.Empty;
            try
            {
                imageName = await UploadedFile(model);

                tblTestimonials tm = new tblTestimonials();
                tm.TestimonialText = model.TestimonialText;
                tm.TestimonialBy = model.TestimonialBy;
                tm.StudentImage = imageName;
                tm.CreatedOn = DateTime.Now.ConvertToIndianTime();
                await entity.tblTestimonials.AddAsync(tm);
                await entity.SaveChangesAsync();
                notyf.Success("Testimonials Added Successfully");

                ModelState.Clear();

            }
            catch (Exception e)
            {
                notyf.Error("Error Occured while uploading testimonials");                
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id > 0)
                {
                    var data = await entity.tblTestimonials.Where(x => x.TestimonailID == id).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        RemoveImage(data.StudentImage);

                        entity.tblTestimonials.Remove(data);
                        await entity.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return RedirectToAction("Index");
        }
        

        #region AddImage
        private async Task<string> UploadedFile(TestimonialDTO model)
        {
            string uniqueFileName = null;

            if (model.StudentImage != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "Images\\Testimonials");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.StudentImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.StudentImage.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region RemoveImage

        private bool RemoveImage(string name)
        {
            var imagePath = Path.Combine(hostEnvironment.WebRootPath, "Images\\Blog", name);
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
