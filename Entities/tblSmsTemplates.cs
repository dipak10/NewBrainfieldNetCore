using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblSmsTemplates
	{
		[Key]
		public int SmsTemplateID { get; set; }
		public string Tempate { get; set; }
	}
}
