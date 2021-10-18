using System;
using System.ComponentModel.DataAnnotations;

namespace NewBrainfieldNetCore.Viewmodels.Cart
{
    public class SpecialPriceIndexViewModel
    {
        [Key]
        public int SpecialPriceId { get; set; }
        public int ExamId { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Special Selling Price in USD")]
        public decimal SpclSellingPriceUSD { get; set; }

        [Display(Name = "Special Selling Price in INR")]
        public decimal SpclSellingPriceINR { get; set; }

        [Display(Name = "Is Active?")]
        public bool IsActive { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Exam Name")]
        public string ExamName { get; set; }

        [Display(Name = "Type")]
        public string Type { get; set; }


        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public string ActiveDeactiveText
        {
            get
            {
                if (IsActive == true)
                {
                    return "<a href='../../SpecialPrice/Deactivate/'" + SpecialPriceId + "'' class='btn btn-warning'> Deactivate </a>";
                }
                else
                {
                    return "<a href='../../SpecialPrice/Activate/'" + SpecialPriceId + "'' class='btn btn-success'> Active </a>";
                }
            }
        }
    }
}
