using Microsoft.EntityFrameworkCore;
using System;

namespace NewBrainfieldNetCore.Entities
{
    [Keyless]
    public class uspGetAllOrders
    {
        public int OrderID { get; set; }
        public string OrderNo { get; set; }
        public decimal Price { get; set; }
        public DateTime PlacedOn { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        //public bool Payable { get; set; }
        public string PhoneNumber { get; set; }        
        public string ExamName { get; set; }        
        public int ExamID { get; set; }
        public int UserID { get; set; }      
        public decimal SellPrice { get; set; }
    }
}
