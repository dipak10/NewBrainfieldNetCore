using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public class tblProducts
	{
		[Key]
		public int ProductID { get; set; }
		public int ProductTypeID { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public string ImageName { get; set; }
		public decimal MarkPrice { get; set; }
		public decimal SellPrice { get; set; }
		public bool IsFeature { get; set; }
		public bool IsAppOnly { get; set; }
		public bool Active { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
