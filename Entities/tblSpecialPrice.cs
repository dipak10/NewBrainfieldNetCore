using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblSpecialPrice
	{
		[Key]
		public int SpecialPriceID { get; set; }
		public int ExamID { get; set; }
		public DateTime StartDate { get; set; }
		public decimal SpclSellingPrice { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime EndDate { get; set; }
		public string Type { get; set; }
		public int PackageMasterID { get; set; }
	}
}
