using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Entities
{
    public class tblVideoPlans
    {
        [Key]
        public int VideoPlansID { get; set; }
        public int VideoPlanTenture { get; set; }
        public decimal PlanPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
