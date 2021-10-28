using NewBrainfieldNetCore.Entities;
using System.Collections.Generic;

namespace NewBrainfieldNetCore.Viewmodels.Menu
{
    public class MenuViewModel
    {
        public List<tblExamCategory> ExamCategories { get; set; }

        public List<tblStudyMaterialCategories> StudyMaterialCategories { get; set; }
    }
}
