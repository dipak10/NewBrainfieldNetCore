using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using System;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class GalleryController : Controller
    {
        private readonly ApplicationContext applicationContext;

        public GalleryController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
  
        public async Task<IActionResult> Index()
        {
            try
            {
                var gallery = await applicationContext.tblGallery.ToListAsync();
                return View(gallery);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return RedirectToAction("Error", "Home");
            }
            finally
            {
                Dispose();
            }
        }
    }
}
