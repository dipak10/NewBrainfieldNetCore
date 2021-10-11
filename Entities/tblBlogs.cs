using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblBlogs
    {
        [Key]
        public int BlogId { get; set; }        
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public string BlogImage { get; set; }       
        public bool IsActive { get; set; }
        public bool IsAppOnly { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
