using Microsoft.AspNetCore.Http;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Dto
{
    public class StudyMaterialFilesDTO
    {
        [Key]
        public int StudyMaterialId { get; set; }
        [Required]
        public int StudyMaterialCategoryID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile FileName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public IFormFile ImageName { get; set; }        
        public DateTime CreatedOn { get; set; }

        public List<tblStudyMaterialCategories> StudyMaterialCategories { get; set; } 
    }
}
