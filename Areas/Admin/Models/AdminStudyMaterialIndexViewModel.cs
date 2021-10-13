using Microsoft.AspNetCore.Http;
using NewBrainfieldNetCore.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Areas.Admin.Models
{
    public class AdminStudyMaterialIndexViewModel
    {
        [Key]
        public int StudyMaterialId { get; set; }
        [Display(Name = "Category")]
        public string StudyCategoryName { get; set; }
        [Display(Name = "Title of file")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Study material file (.zip only)")]
        public IFormFile UploadFileName { get; set; }

        [Display(Name = "Cover pic image")]
        public IFormFile UplodCoverPic { get; set; }

        [Display(Name = "Price (In INR)")]
        public decimal Price { get; set; }

        public string DisplayFileName { get; set; }

        public int StudyMaterialCategoryID { get; set; }

        public List<tblStudyMaterialCategories> StudyMaterialCategories { get; set; }
    }
}
