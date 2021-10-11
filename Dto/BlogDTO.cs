using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class BlogDTO
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        [Display(Name ="Blog title")]
        public string BlogTitle { get; set; }
      
        [Required]
        [Display(Name = "Blog content")]
        public string BlogContent { get; set; }


        [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Profile Picture")]
        public IFormFile BlogImage { get; set; }


        public DateTime CreatedOn { get; set; }
    }
}
