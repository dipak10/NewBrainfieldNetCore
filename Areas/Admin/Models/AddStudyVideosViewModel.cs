using NewBrainfieldNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Areas.Admin.Models
{
    public class AddStudyVideosViewModel
    {
        [Key]
        public int VideosID { get; set; }

        [Required]
        [Display(Name = "Video Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Video URL")]
        public string VideoURL { get; set; }

        [Display(Name = "Free Video?")]
        public bool IsFree { get; set; }

        //[Required]
        //[Display(Name = "Category")]
        //[EnumDataType(typeof(VideoCategories))]
        //public VideoCategories Category { get; set; }        

        public DateTime CreatedOn { get; set; } = DateTime.Now.ConvertToIndianTime();

        #region Helper
        public enum VideoCategories
        {
            Phyiscs = 1,
            Chemistry = 2,
            Biology = 3,
        }

        public string DisplayFree()
        {
            return IsFree ? "Yes" : "No";
        }

        #endregion Helper
    }
}
