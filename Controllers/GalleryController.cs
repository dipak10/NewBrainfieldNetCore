using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
   
    public class GalleryController : Controller
    {

        public GalleryController()
        {

        }

   
        public IActionResult Index()
        {
            return View();
        }
    }
}
