using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Viewmodels
{
    public class HomePageViewModel
    {
        public List<tblBlogs> Blogs { get; set; }
        public List<tblTestimonials> Testimonials { get; set; }
    }
}
