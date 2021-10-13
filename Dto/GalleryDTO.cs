using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class GalleryDTO
    {
        [Key]
        public int GalleryID { get; set; }
        [Required]
        public IFormFile[] Images { get; set; }
    }
}
