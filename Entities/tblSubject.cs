using NewBrainfieldNetCore.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public class tblSubject
    {
        [Key]
        public int SubjectID { get; set; }
        public string SubjectName { get; set; }
        public int StandardID { get; set; }

        [ForeignKey("StandardID")]

        public virtual ICollection<tblStandard> Standards { get; set; }

        
    }
}
