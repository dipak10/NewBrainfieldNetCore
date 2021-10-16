using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblProductsAttributes
	{
		[Key]
		public int ProductAttributesID { get; set; }
		public string ProductAttributeName { get; set; }
	}
}
