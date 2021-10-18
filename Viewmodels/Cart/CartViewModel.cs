using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels.Cart
{
    public class CartViewModel
    {
        public int UserCartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal SellPrice { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
    }
}
