using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class DownloadController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly INotyfService _notyf;
        private readonly ICommonService _commonService;
        private readonly IHomeService _homeService;
        private readonly ApplicationContext _entity;

        public DownloadController(ILogger<BlogController> logger, INotyfService notyf, ICommonService commonService,
            IHomeService homeService, ApplicationContext entity)
        {
            _logger = logger;
            _notyf = notyf;
            _commonService = commonService;
            _homeService = homeService;
            _entity = entity;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<StudentDownloadsViewModel> studentDownloads = null;

            if (GlobalVariables.UserId > 0)
            {
                studentDownloads = (from x in _entity.tblDownloads
                                    join y in _entity.tblDownloadCategory on x.Section equals y.DownloadCategoryID
                                    select new StudentDownloadsViewModel
                                    {
                                        DownloadsID = x.DownloadsID,
                                        Title = x.Title,
                                        DownloadCategoryName = y.DownloadCategoryName,
                                        FileName = x.FileName,
                                        IsFree = x.IsFree,
                                        UploadOn = x.UploadOn
                                    }).ToList();
            }
            else
            {
                studentDownloads = (from x in _entity.tblDownloads
                                    join y in _entity.tblDownloadCategory on x.Section equals y.DownloadCategoryID
                                    select new StudentDownloadsViewModel
                                    {
                                        DownloadsID = x.DownloadsID,
                                        Title = x.Title,
                                        DownloadCategoryName = y.DownloadCategoryName,
                                        FileName = x.FileName,
                                        IsFree = x.IsFree,
                                        UploadOn = x.UploadOn
                                    }).Where(x => x.IsFree == true).ToList();
            }

            return View(studentDownloads);
        }

        public IActionResult Index(int? FilterBy)
        {
            var categories = _entity.tblDownloadCategory.ToList();

            var books = new List<tblDownloads>();
            if (GlobalVariables.UserId > 0)
            {
                books = _entity.tblDownloads.Where(x => x.IsFree == true).OrderByDescending(x => x.UploadOn).ToList();
            }
            else
            {
                books = _entity.tblDownloads.OrderByDescending(x => x.UploadOn).ToList();
            }

            if (FilterBy > 0)
            {
                books = books.Where(x => x.Section == FilterBy).ToList();
            }

            books = CreateLink(books);

            return View(books);
        }

        private List<tblDownloads> CreateLink(List<tblDownloads> tbls)
        {
            List<tblDownloads> downloads = new List<tblDownloads>();
            foreach (var r in tbls)
            {
                downloads.Add(new tblDownloads { DownloadsID = r.DownloadsID, Title = r.Title, UploadOn = r.UploadOn, FileName = r.FileName + ".zip" });
            }

            return downloads;
        }
    }
}
