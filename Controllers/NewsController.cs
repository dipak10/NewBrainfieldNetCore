using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Models;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    [Route("Student/")]
    public class NewsController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly INotyfService _notyf;
        private readonly ICommonService _commonService;
        private readonly IHomeService _homeService;
        private readonly ApplicationContext _context;

        public NewsController(ILogger<BlogController> logger, INotyfService notyf, ICommonService commonService,
            IHomeService homeService, ApplicationContext context)
        {
            _logger = logger;
            _notyf = notyf;
            _commonService = commonService;
            _homeService = homeService;
            _context = context;
        }
        [Route("ReadNews")]
        public async Task<IActionResult> Index()
        {
            List<tblNews> news = await _context.tblNews.ToListAsync();
            return View(news);
        }

        [Route("Read/News/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                if (id > 0)
                {
                    tblNews data = await _context.tblNews.Where(x => x.NewsID == id).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        return View(data);
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Index");
            }
        }
    }
}
