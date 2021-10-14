using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Dto
{
    public class ChapterDTO
    {
        [Key]
        public int ChapterID { get; set; }

        [Required]
        public int StandardID { get; set; }

        [Required]
        public int SubjectID { get; set; }

        [Required]
        [Display(Name = "Chapter name")]
        public string ChapterName { get; set; }

        [ForeignKey("SubjectID")]
        public virtual List<tblSubject> Subjects { get; set; }

        public List<StandardDTO> Standards { get; set; }
    }
}
