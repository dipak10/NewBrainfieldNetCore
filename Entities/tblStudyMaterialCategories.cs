using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblStudyMaterialCategories
    {
        [Key]
        public int StudyMaterialCategoryID { get; set; }
        public string StudyMaterialCategoryName { get; set; }
    }
}
