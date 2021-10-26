using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Models;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;
        private readonly ICommonService _commonService;
        private readonly IHomeService _homeService;
        private readonly ApplicationContext _context;

        public HomeController(ILogger<HomeController> logger, INotyfService notyf, ICommonService commonService,
            IHomeService homeService, ApplicationContext context)
        {
            _logger = logger;
            _notyf = notyf;
            _commonService = commonService;
            _homeService = homeService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                //var data = await _commonService.GetCurrentUser();
                //if (data != null)
                //{
                //    Helpers.GlobalVariables.UserId = data.UserID;
                //}
                               
                var news = await _homeService.GetHomePageNews();
                var testimonials = await _homeService.GetTestimonials();
                var featuredExams = await _homeService.GetFeaturedExams();

                var model = new HomePageViewModel
                {
                    News = news,
                    Testimonials = testimonials,
                    FeaturedExams = featuredExams
                };
              
                return View(model);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error Occured {e.InnerException}");
            }
            finally
            {
                Dispose();
            }
            return RedirectToAction("Error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
