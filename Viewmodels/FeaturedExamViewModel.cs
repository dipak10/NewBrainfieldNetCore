using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class FeaturedExamViewModel
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public string ExamDescription { get; set; }
        public decimal SellPrice { get; set; }
        public decimal MarkPrice { get; set; }
        public string Image { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
