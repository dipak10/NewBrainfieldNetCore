using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewBrainfieldNetCore.Models;
using NewBrainfieldNetCore.Services.Interfaces;
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

        public HomeController(ILogger<HomeController> logger, INotyfService notyf, ICommonService commonService)
        {
            _logger = logger;
            _notyf = notyf;
            _commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _commonService.GetCurrentUser();
                User
                if (data != null)
                {
                    _logger.LogInformation("Data found");
                }
            }
            catch(Exception e)
            {
                _logger.LogError("Error Occured");
            }
            finally
            {
                Dispose();
            }

            return View();
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
