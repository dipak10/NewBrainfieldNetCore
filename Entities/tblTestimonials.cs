using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public class tblTestimonials
    {
        [Key]
        public int TestimonailID { get; set; }
        public string TestimonialBy { get; set; }
        public string TestimonialText { get; set; }
        public string StudentImage { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
