using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public class tblExamCategory
    {
        [Key]
        public int ExamCategoryID { get; set; }
        public string ExamCategoryName { get; set; }        
    }
}
