using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels.StudentExamViewModels
{
    public class SingleExamViewModel
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public decimal SellPrice { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string Duration { get; set; }
        public string Subject { get; set; }
        public string ImageName { get; set; }
        public string DifficultyLevel { get; set; }
        public int Students { get; set; }

        public AddToCartViewModel AddToCartViewModel { get; set; }
    }
}
