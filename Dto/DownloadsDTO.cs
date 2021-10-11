using Microsoft.AspNetCore.Http;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class DownloadsDTO
    {
        [Key]
        public int DownloadsID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public IFormFile FileName { get; set; }
        [Required]
        public bool IsFree { get; set; }
        [Required]
        public DateTime UploadOn { get; set; }
        [Required]
        public int Section { get; set; }

        public string CategoryName { get; set; }

        public string DisplayFileName { get; set; }

        public List<tblDownloadCategory>  DownloadCategories { get; set; }
    }
}
