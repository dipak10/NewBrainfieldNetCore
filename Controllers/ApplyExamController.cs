using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class ApplyExamController : Controller
    {
        public IActionResult Index(int ExamId, int OrderId)
        {

            return View();
        }
    }
}
