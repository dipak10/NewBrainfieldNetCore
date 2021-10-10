using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Dto
{
    public class SubjectDTO
    {
        

        [Key]
        public int SubjectID { get; set; }
        [Required]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }
        [Required]
        [Display(Name = "Standard")]
        public int StandardID { get; set; }

        public List<StandardDTO> Standards { get; set; }

        
        [ForeignKey("StandardID")]
        public virtual ICollection<tblStandard> _Standards { get; set; }
    }
}
