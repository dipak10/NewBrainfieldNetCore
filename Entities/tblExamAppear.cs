using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{   
	public class tblExamAppear
	{
		[Key]
		public int ExamAppearID { get; set; }
		public int UserID { get; set; }
		public int ExamID { get; set; }
		public int OrderID { get; set; }
		public int Appear { get; set; }
		public DateTime LastUpdated { get; set; }	
	}
}
