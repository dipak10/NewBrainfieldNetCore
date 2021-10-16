using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblPackageMaster
	{
		[Key]
		public int PackageMasterID { get; set; }
		public string PackageName { get; set; }
		public decimal MarkPrice { get; set; }
		public decimal SellPrice { get; set; }
		public string ImageName { get; set; }
		public string Description { get; set; }
		public bool Active { get; set; }
		public decimal AppOnlyDiscount { get; set; }
		public bool AppOnlyDisc { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
