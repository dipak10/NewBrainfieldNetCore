using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblDownloads
    {
        [Key]
        public int DownloadsID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }        
        public bool IsFree { get; set; }
        public DateTime UploadOn { get; set; }
        public int Section { get; set; }
    }
}
