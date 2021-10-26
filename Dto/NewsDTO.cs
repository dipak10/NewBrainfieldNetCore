using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class NewsDTO
    {
        [Key]
        public int NewsID { get; set; }
        [Required]
        [Display(Name = "News Headline")]
        public string NewsHeadline { get; set; }

        [Required]
        [Display(Name = "News Short Detail")]
        public string NewsShortDetail { get; set; }

        [Required]
        [Display(Name = "News Detail")]
        public string NewsDetail { get; set; }
        [Required]
        [Display(Name = "News Image")]
        public IFormFile ImageName { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
