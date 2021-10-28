using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels.Menu
{
    public class MenuViewModel
    {
        public List<tblExamCategory> ExamCategories { get; set; }

        public List<tblStudyMaterialCategories> StudyMaterialCategories { get; set; }
    }
}
