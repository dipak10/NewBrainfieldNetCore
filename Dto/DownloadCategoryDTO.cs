using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class DownloadCategoryDTO
    {
        [Key]
        public int DownloadCategoryID { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string DownloadCategoryName { get; set; }
    }
}
