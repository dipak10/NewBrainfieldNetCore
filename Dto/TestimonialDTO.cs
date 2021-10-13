using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class TestimonialDTO
    {
        [Key]
        public int TestimonialID { get; set; }
        [Required]
        public string TestimonialBy { get; set; }
        [Required]
        public string TestimonialText { get; set; }
        [Required]
        public IFormFile StudentImage { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
