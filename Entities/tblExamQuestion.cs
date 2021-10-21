using System;
using System.ComponentModel.DataAnnotations;

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
