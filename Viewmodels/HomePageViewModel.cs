using NewBrainfieldNetCore.Entities;
using System.Collections.Generic;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class HomePageViewModel
    {
        public List<tblNews> News { get; set; }
        public List<tblTestimonials> Testimonials { get; set; }
        public List<FeaturedExamViewModel> FeaturedExams { get; set; }
    }
}
