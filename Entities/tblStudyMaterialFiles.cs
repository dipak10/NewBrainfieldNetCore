using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public class tblStudyMaterialFiles
    {
        [Key]
        public int StudyMaterialFilesID { get; set; }
        public int StudyMaterialCategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
