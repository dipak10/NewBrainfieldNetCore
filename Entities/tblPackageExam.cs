using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblPackageExam
	{
		[Key]
		public int PackageExamID { get; set; }
		public int PackageID { get; set; }
		public int ExamID { get; set; }
		public DateTime AddedOn { get; set; }
	}
}
