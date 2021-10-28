using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using System;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ApplicationContext _applicationContext;
        private readonly INotyfService _notyf;

        public ContactUsController(ApplicationContext applicationContext, INotyfService notyf)
        {
            _applicationContext = applicationContext;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(tblContactUs model)
        {
            try
            {
                tblContactUs tbl = new tblContactUs();
                tbl.Name = model.Name;
                tbl.Email = model.Email;
                tbl.Message = model.Message;
                tbl.PhoneNumber = model.PhoneNumber;
                tbl.CreatedOn = DateTime.Now.ConvertToIndianTime();
                _applicationContext.tblContactUs.Add(tbl);
                await _applicationContext.SaveChangesAsync();

                _notyf.Success("Enquiry Sent Successfully");

                ModelState.Clear();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                _notyf.Error("Error occured while sending enquiry");
            }
            return View();
        }
    }
}
