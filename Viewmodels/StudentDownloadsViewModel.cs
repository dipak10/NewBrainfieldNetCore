using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class StudentDownloadsViewModel
    {
        public int DownloadsID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public bool IsFree { get; set; }
        public DateTime UploadOn { get; set; }
        public int Section { get; set; }
        public int DownloadCategoryID { get; set; }
        public string DownloadCategoryName { get; set; }
    }
}
