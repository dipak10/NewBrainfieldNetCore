using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public class tblExamMaster
    {
        [Key]
        public int ExamID { get; set; }
        public int StandardID { get; set; }
        public string ExamName { get; set; }
        public int DifficultyLevelID { get; set; }
        public int Repetation { get; set; }
        public string ImageName { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public bool IsFeatured { get; set; }
        public decimal SellPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public string TimeDuration { get; set; }
        public int ExamCategoryID { get; set; }
        public decimal AppOnlyDiscount { get; set; }
        public bool AppOnlyDisc { get; set; }
        public DateTime CreatedOn { get; set; }     
    }
}
