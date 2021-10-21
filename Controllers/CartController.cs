using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
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

        public string Type { get; set; }
        public int ProductId { get; set; }

        public CartController(ApplicationContext entity, INotyfService notyf)
        {
            this.entity = entity;
            this.notyf = notyf;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(SingleProductViewModel viewModel)
        {
            Type = viewModel.AddToCartViewModel.Type;
            ProductId = viewModel.AddToCartViewModel.ProductID;

            try
            {
                if (!string.IsNullOrEmpty(viewModel.AddToCartViewModel.Type))
                {
                    GlobalVariables.ProductType = Type;
                    GlobalVariables.ProductId = ProductId;
                }

                var userid = Convert.ToInt32(1);
                var ucart = await entity.tblUserCart.Where(ca => ca.UserID == userid && ca.ExamID == ProductId && ca.Type == Type).FirstOrDefaultAsync();
                if (ucart != null)
                {
                    notyf.Information("There is already a product in cart");
                    return RedirectToAction("MyCart", "UserAccount");
                }
                else
                {
                    tblUserCart u = new tblUserCart();
                    u.ExamID = ProductId;
                    u.UserID = userid;
                    u.Type = GlobalVariables.ProductType;
                    u.DateAdded = DateTime.Now;
                    await entity.tblUserCart.AddAsync(u);
                    await entity.SaveChangesAsync();
                    notyf.Success("Product Added to cart successfully");
                    return RedirectToAction("MyCart", "UserAccount");
                }
            }
            catch (Exception e)
            {
                notyf.Error("Error While Adding Product to cart");
            }
            finally
            {
                Dispose();
            }
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var getdata = await entity.tblUserCart.Where(x => x.UserCartID == id).FirstOrDefaultAsync();
            if (getdata != null)
            {
                entity.tblUserCart.Remove(getdata);
                await entity.SaveChangesAsync();
                return RedirectToAction("MyCart", "UserAccount");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProceedPayment()
        {
            return RedirectToAction("Index", "Payment");            
        }
    }
}
