using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Areas.Admin.Models;
using NewBrainfieldNetCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Helpers
{
    public class QueryHelper
    {
        private ApplicationContext applicationContext;

        public QueryHelper(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<List<AdminExamsIndexViewModel>> GetAllExams()
        {
            return await (from x in applicationContext.tblExamMaster
                          join y in applicationContext.tblExamCategory on
                          x.ExamCategoryID equals y.ExamCategoryID
                          join z in applicationContext.tblExamSubject on
                          x.ExamID equals z.ExamID
                          join p in applicationContext.tblSubject on
                          z.SubjectID equals p.SubjectID
                          join q in applicationContext.tblStandard on
                          x.StandardID equals q.StandardID
                          select new AdminExamsIndexViewModel
                          {
                              ExamID = x.ExamID,
                              ExamName = x.ExamName,
                              MarkPrice = x.MarkPrice,
                              SellPrice = x.SellPrice,
                              TimeDuration = x.TimeDuration,
                              Repetation = x.Repetation,
                              Notes = x.Notes,
                              StandardName = q.StandardName,
                              SubjectName = p.SubjectName,
                              CategoryName = y.ExamCategoryName,
                              IsFeatured = x.IsFeatured,
                              CreatedOn = x.CreatedOn
                          }).ToListAsync();
        }
    }
}
