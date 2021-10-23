using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Entities
{
    [Keyless]
    public class uspGetStudentExam
    {        
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public int Appear { get; set; }
        public int Repetation { get; set; }
        public int OrderId { get; set; }
    }
}
