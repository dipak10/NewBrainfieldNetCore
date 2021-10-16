using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblNews
	{	
		[Key]
		public int NewsID { get; set; }
		public string NewsHeadline { get; set; }
		public string NewsDetail { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsActive { get; set; }
	}
}
