using Microsoft.AspNetCore.Mvc.Rendering;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;

namespace NewBrainfieldNetCore.Areas.Admin.Models
{
    public class QuestionMasterModel
    {
        public int QuestionMasterID { get; set; }
        public int ExamID { get; set; }
        public int StandardID { get; set; }
        public int SubjectID { get; set; }
        public int ChapterID { get; set; }
//        [AllowHtml]
        public string Question { get; set; }
        public string Type { get; set; }
        public int Mark { get; set; }

        public decimal NegativeMark { get; set; }

        public DateTime CreatedOn { get; set; }
        //[AllowHtml]
        public string OptionA { get; set; }
        //[AllowHtml]
        public string OptionB { get; set; }
        //[AllowHtml]
        public string OptionC { get; set; }
        //[AllowHtml]
        public string OptionD { get; set; }
        //[AllowHtml]
        public string Explanation { get; set; }
        public string CorrectAnswer { get; set; }

        public List<SelectListItem> ExamMasters { get; set; }

        public List<StandardDTO> Standards { get; set; }

        public List<tblSubject> Subjects { get; set; }

        public List<tblChapters> Chapters { get; set; }
    }
}
