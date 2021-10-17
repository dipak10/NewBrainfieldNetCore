﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public class tblQuestionMaster
    {
        public int QuestionMasterID { get; set; }
        public int StandardID { get; set; }
        public int SubjectID { get; set; }
        public int ChapterID { get; set; }
        public string Question { get; set; }
        public string Type { get; set; }
        public int Mark { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal NegativeMark { get; set; }
    }

}