using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblExamSubject
	{
		[Key]
		public int ExamSubjectID { get; set; }
		public int ExamID { get; set; }
		public int SubjectID { get; set; }
	}
}
