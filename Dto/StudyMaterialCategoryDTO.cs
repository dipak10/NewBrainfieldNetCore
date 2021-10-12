using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class StudyMaterialCategoryDTO
    {
        [Key]
        public int StudyMaterialCategoryID { get; set; }
        [Required]
        public string StudyMaterialCategoryName { get; set; }
    }
}
