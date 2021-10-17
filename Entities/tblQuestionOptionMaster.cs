﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
	public class tblQuestionOptionMaster
	{
		[Key]
		public int OptionMasterID { get; set; }
		public int QuestionID { get; set; }
		public string OptionA { get; set; }
		public string OptionB { get; set; }
		public string OptionC { get; set; }
		public string OptionD { get; set; }
		public string Explanation { get; set; }
		public string CorrectAnswer { get; set; }
	}
}