using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class StudentStudyMaterialCatwiseIndexViewModel
    {
        [Key]
        public int StudyMaterialId { get; set; }
        [Display(Name = "Category")]
        public string StudyCategoryName { get; set; }
        [Display(Name = "Title of file")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Price (In INR)")]
        public decimal Price { get; set; }
        [Display(Name = "Image")]
        public string ImageName { get; set; }
    }
}
