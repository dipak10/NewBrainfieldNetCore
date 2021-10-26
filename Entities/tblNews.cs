using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public class tblNews
	{	
		[Key]
		public int NewsID { get; set; }
		public string NewsHeadline { get; set; }
		public string NewsShortDetail { get; set; }
		public string NewsDetail { get; set; }
		public string ImageName { get; set; }
		public DateTime CreatedDate { get; set; }
		public bool IsActive { get; set; }
	}
}
