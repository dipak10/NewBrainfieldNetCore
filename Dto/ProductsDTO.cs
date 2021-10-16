using Microsoft.AspNetCore.Http;
using NewBrainfieldNetCore.Common;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Dto
{
    public partial class ProductsDTO
    {        
        [Key]
        public int ProductID { get; set; }
        public int ProductTypeID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageName { get; set; }
        public decimal MarkPrice { get; set; }
        public decimal SellPrice { get; set; }
        public bool IsFeature { get; set; }
        public bool IsAppOnly { get; set; }
        public bool IsActive { get; set; }        
        public DateTime CreatedOn { get; set; }


        public int ExamCategoryID { get; set; }
        public int StandardID { get; set; }
        public int SubjectID { get; set; }
        public int DifficultyLevelID { get; set; }
        public int Duration { get; set; }
        public int Repetation { get; set; }
        public string Notes { get; set; }


        #region List
        public List<tblExamCategory> ExamCategories { get; set; }
        public List<StandardDTO> Standards { get; set; }
        public List<tblSubject> Subjects { get; set; }
        #endregion List
    }
}
