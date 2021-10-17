using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels.StudentExamViewModels
{
    public class CategorywiseExamListViewModel
    {
        public List<tblExamMaster> Exams { get; set; }

        public string CategoryName { get; set; }
    }
}
