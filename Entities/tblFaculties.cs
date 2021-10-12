using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    public partial class tblFaculties
    {
        [Key]
        public int FacultyID { get; set; }
        public string FacultyName { get; set; }
        public string SubjectName { get; set; }
        public string Details { get; set; }
        public string Experience { get; set; }
        public string Photo { get; set; }
    }
}
