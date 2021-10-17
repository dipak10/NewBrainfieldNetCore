using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Models
{
    public class AddPackageViewModel
    {
        [Key]
        public int PackageMasterId { get; set; }

        [Display(Name = "Package Name")]
        [Required(ErrorMessage = "Please enter Package Name")]
        public string PackageName { get; set; }
        
        [Display(Name = "Mark Price")]
        public decimal MarkPrice { get; set; }

        [Display(Name = "Selling Price")]
        [Required(ErrorMessage = "Please enter selling price in INR")]
        public decimal SellPrice { get; set; }
        
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        private DateTime CreatedOn { get; set; }

        //public int[] ExamId { get; set; }

        
        public List<SelectListItem> ExamMasters { get; set; }

        [Required(ErrorMessage = "Please select image")]
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }


        [Display(Name = "Exam Master")]
        [Required(ErrorMessage = "Please select exam")]
        public IEnumerable<string> SelectedExams { get; set; }
    }
}
