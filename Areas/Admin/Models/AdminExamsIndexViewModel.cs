using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Models
{
    public class AdminExamsIndexViewModel
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public decimal SellPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public string TimeDuration { get; set; }
        public int Repetation { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public string StandardName { get; set; }
        public string SubjectName { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsDeleted { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
