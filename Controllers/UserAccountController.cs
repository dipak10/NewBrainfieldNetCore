using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Viewmodels.Cart;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Viewmodels;

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

        [HttpGet]
        [Route("MyExams")]
        public async Task<IActionResult> MyExams()
        {
            try
            {
                var data = await entity.UspGetStudentExam.FromSqlRaw("uspGetStudentExam {0}", 2).ToListAsync();
                return View(data);
            }
            catch (Exception e)
            {
                notyf.Error("Error occured while fetching exams");

                return RedirectToAction("Index", "Home");
            }
            finally
            {
                Dispose();
            }
        }


        #region Helper

        [HttpGet]
        [Route("GetOrderDetail")]
        public async Task<JsonResult> GetOrderDetail(int id)
        {
            var orderDetails = await (from x in entity.tblOrderMaster
                                      where x.OrderID == id
                                      select new OrderViewModel
                                      {
                                          OrderNo = x.OrderNo,
                                          PlacedOn = x.PlacedOn,
                                          Price = x.Price
                                      }).FirstOrDefaultAsync();

            return Json(orderDetails);
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
        #endregion Helper
    }
}
