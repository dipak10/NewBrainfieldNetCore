using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Dto
{
    public class ExamCategoryDTO
    {
        [Key]
        public int ExamCategoryID { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name")]
        [Display(Name = "Category Name")]
        public string ExamCategoryName { get; set; }               
    }
}
