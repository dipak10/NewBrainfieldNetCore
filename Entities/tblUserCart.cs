using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public class tblUserCart
    {
        [Key]
        public int UserCartID { get; set; }
        public int UserID { get; set; }
        public int ExamID { get; set; }
        public DateTime DateAdded { get; set; }
        public string Type { get; set; }
    }
}
