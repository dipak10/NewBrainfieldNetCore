using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaseController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper mapper;

        public BaseController(ApplicationContext entity, INotyfService notyf, IWebHostEnvironment hostEnvironment, IMapper mapper)
        {
            this.entity = entity;
            this.notyf = notyf;
            this._hostEnvironment = hostEnvironment;
            this.mapper = mapper;
        }
    }
}
