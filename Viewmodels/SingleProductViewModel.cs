using NewBrainfieldNetCore.Viewmodels.StudentExamViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class SingleProductViewModel
    {
        public SingleExamViewModel SingleExamViewModel { get; set; }

        public AddToCartViewModel AddToCartViewModel { get; set; }
    }
}
