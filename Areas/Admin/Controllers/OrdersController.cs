using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using System;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly ApplicationContext _entity;
        private readonly INotyfService _notyf;

        public OrdersController(ApplicationContext entity,
            INotyfService notyf)
        {
            _entity = entity;
            _notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _entity.uspGetAllOrders.FromSqlRaw("uspGetAllOrders").ToListAsync();
                return View(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.InnerException);
                _notyf.Error("Something went wrong");
                return RedirectToAction("Dashboard", "Home");
            }
            finally
            {
                Dispose();
            }
        }
    }
}
