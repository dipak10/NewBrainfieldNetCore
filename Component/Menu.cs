using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Viewmodels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Component
{
    public class Menu : ViewComponent
    {
        private readonly ApplicationContext applicationContext;

        public Menu(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var examCategories = await applicationContext.tblExamCategory.OrderBy(x => x.ExamCategoryName).ToListAsync();

            var studymaterialCategories = await applicationContext.tblStudyMaterialCategories.OrderBy(x => x.StudyMaterialCategoryName).ToListAsync();

            var model = new MenuViewModel()
            {
                ExamCategories = examCategories,
                StudyMaterialCategories = studymaterialCategories
            };

            return View(model);            
        }
    }
}
