using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Viewmodels.Cart;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    [Route("Student/UserAccount/")]
    public class UserAccountController : Controller
    {
        private readonly ApplicationContext entity;
        private readonly INotyfService notyf;



        public UserAccountController(ApplicationContext entity, INotyfService notyf)
        {
            this.entity = entity;
            this.notyf = notyf;
        }

        [HttpGet]
        [Route("MyCart")]
        public async Task<IActionResult> MyCart()
        {
            try
            {
                GlobalVariables.UserId = 1;

                QueryHelper QueryHelper = new QueryHelper(applicationContext: entity);
          
                IEnumerable<CartViewModel> carts = await QueryHelper.GetCart();

                if (carts.Count() == 0) return RedirectToAction("Index", "Home");

                CalculateGrandTotal(carts);
                       
                return View(carts);
            }
            catch (Exception e)
            {
                notyf.Error("Error in cart");
                return RedirectToAction("Index", "Home");
            }
            finally
            {
                Dispose();
            }
        }

        private void CalculateGrandTotal(IEnumerable<CartViewModel> carts)
        {
            decimal gt = 0;
            foreach (var r in carts)
            {
                gt = Convert.ToDecimal(gt + r.SellPrice);
                GlobalVariables.GrandTotal = gt;
            }
        }
    }
}
