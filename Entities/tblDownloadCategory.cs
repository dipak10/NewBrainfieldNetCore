using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblDownloadCategory
    {
        [Key]
        public int DownloadCategoryID { get; set; }
        public string DownloadCategoryName { get; set; }
    }
}
