using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Dto
{
    public class FacultiesDTO
    {
        [Key]
        public int FacultyID { get; set; }
        [Required]
        public string FacultyName { get; set; }
        [Required]
        public string SubjectName { get; set; }
        [Required]
        public string Details { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
