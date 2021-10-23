using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
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
        private readonly ApplicationContext context;

        public HomeController(ILogger<HomeController> logger,
            INotyfService notyf,
            ICommonService commonService, ApplicationContext context)
        {
            _logger = logger;
            _notyf = notyf;
            _commonService = commonService;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _commonService.GetCurrentUser();
                if (data != null)
                {
                    Helpers.GlobalVariables.UserId = data.UserID;
                }                              
            }
            catch (Exception e)
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
