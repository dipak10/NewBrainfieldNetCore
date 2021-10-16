using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblOrderMaster
	{
		[Key]
		public int OrderID { get; set; }
		public int UserID { get; set; }
		public int ExamID { get; set; }
		public string OrderNo { get; set; }
		public decimal Price { get; set; }
		public DateTime PlacedOn { get; set; }
		public string Currency { get; set; }
		public string Type { get; set; }
	}
}
