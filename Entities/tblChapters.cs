using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBrainfieldNetCore.Entities
{
    public class tblChapters
    {
        [Key]
        public int ChapterID { get; set; }        
        public int SubjectID { get; set; }        
        public string ChapterName { get; set; }
        [ForeignKey("SubjectID")]
        public virtual List<tblSubject> Subjects { get; set; }
    }
}
