using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblExamMark
	{
		[Key]
		public int ExamResult { get; set; }
		public int ExamID { get; set; }
		public int UserID { get; set; }
		public decimal ObtainMark { get; set; }
		public DateTime DateAdded { get; set; }
	}
}
