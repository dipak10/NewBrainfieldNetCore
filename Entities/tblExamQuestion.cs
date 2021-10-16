using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblExamQuestion
	{
		[Key]
		public int ExamQuestionID { get; set; }
		public int ExamID { get; set; }
		public int QuestionID { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
