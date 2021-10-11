using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Dto
{
    public class AdminAdmissionFeesDTO
    {
        [Key]
        public int FeesId { get; set; }

        [Required(ErrorMessage = "Select Stanadard")]
        [Display(Name = "Standard")]
        public int StandardMasterId { get; set; }

        public string StandaraName { get; set; }

        public List<StandardDTO> StandardMasters { get; set; }

        [Required(ErrorMessage = "Enter Amount")]
        [Display(Name = "Amount")]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Enter Fees for Application")]
        [Display(Name = "Application Amount")]
        [Range(1, 1000000)]
        public decimal AppOnlyAmount { get; set; }
    }
}
