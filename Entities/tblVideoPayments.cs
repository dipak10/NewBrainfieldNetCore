using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public class tblVideoPayments
    {
        [Key]
        public int VideoPaymentID { get; set; }
        public int SubscriptionID { get; set; }
        public decimal Amout { get; set; }
        public DateTime DatePaid { get; set; }
    }
}
