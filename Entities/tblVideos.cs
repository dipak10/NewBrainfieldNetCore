using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblVideos
    {
        [Key]
        public int VideosID { get; set; }
        //public string Category { get; set; }
        public string Title { get; set; }
        public string VideoURL { get; set; }
        public bool IsFree { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
