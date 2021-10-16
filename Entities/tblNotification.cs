using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblNotification
	{
		[Key]
		public int NotificationID { get; set; }
		public string Category { get; set; }
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string Description { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
