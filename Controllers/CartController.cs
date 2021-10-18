using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{

    public class CartController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;

        public CartController(ApplicationContext entity, INotyfService notyf)
        {
            this.entity = entity;
            this.notyf = notyf;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(SingleProductViewModel viewModel)
        {
            try
            {

            }
            catch(Exception e)
            {

            }
            finally
            {
                Dispose();
            }
            return View();
        }
    }
}
