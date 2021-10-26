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
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly INotyfService _notyf;
        private readonly ICommonService _commonService;
        private readonly IHomeService _homeService;
        private readonly ApplicationContext _context;

        public BlogController(ILogger<BlogController> logger, INotyfService notyf, ICommonService commonService,
            IHomeService homeService, ApplicationContext context)
        {
            _logger = logger;
            _notyf = notyf;
            _commonService = commonService;
            _homeService = homeService;
            _context = context;
        }

        [Route("ReadBlogs")]
        public async Task<IActionResult> Index()
        {
            try
            {
                List<tblBlogs> blogs = await _context.tblBlogs.ToListAsync();

                return View(blogs);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured while getting blogs {e.InnerException}");

                return RedirectToAction("Error", "Home");
            }
            finally
            {
                Dispose();
            }
        }

        [Route("Read/Blog/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                if (id > 0)
                {
                    tblBlogs data = await _context.tblBlogs.Where(x => x.BlogId == id).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        return View(data);
                    }
                }
                return RedirectToAction("Error", "Home");
            }
            catch (Exception e)
            {
                Console.Write(e);
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
