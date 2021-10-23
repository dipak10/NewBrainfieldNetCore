using System;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class OrderViewModel
    {
        public string OrderNo { get; set; }
        public decimal Price { get; set; }
        public DateTime PlacedOn { get; set; }
    }
}
