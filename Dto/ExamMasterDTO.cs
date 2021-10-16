using Microsoft.AspNetCore.Http;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Dto
{
    public class ExamMasterDTO
    {
        [Key]
        public int ExamID { get; set; }
        public int StandardID { get; set; }
        public string ExamName { get; set; }
        public int DifficultyLevelID { get; set; }
        public int Repetation { get; set; }
        public IFormFile ImageName { get; set; }
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

        public bool IsActive { get; set; }
        public int SubjectID { get; set; }

        #region List
        public List<tblExamCategory> ExamCategories { get; set; }
        public List<StandardDTO> Standards { get; set; }
        public List<tblSubject> Subjects { get; set; }        
        #endregion List
    }
}
