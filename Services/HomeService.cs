using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using NewBrainfieldNetCore.Viewmodels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationContext context;
        private List<FeaturedExamViewModel> FeaturedExams { get; set; }

        public HomeService(ApplicationContext context)
        {
            this.context = context;
            FeaturedExams = new List<FeaturedExamViewModel>();
        }

        public async Task<List<tblNews>> GetHomePageNews()
        {
            return await context.tblNews.Where(x => x.IsActive == true).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<List<tblTestimonials>> GetTestimonials()
        {
            return await context.tblTestimonials.OrderByDescending(x => x.CreatedOn).ToListAsync();
        }

        public async Task<List<FeaturedExamViewModel>> GetFeaturedExams()
        {
            List<FeaturedExamViewModel> featuredExams = await (from x in context.tblExamMaster
                                                               select new FeaturedExamViewModel
                                                               {
                                                                   ExamID = x.ExamID,
                                                                   ExamName = x.ExamName,
                                                                   ExamDescription = x.Description,
                                                                   Image = x.ImageName,
                                                                   MarkPrice = x.MarkPrice,
                                                                   SellPrice = x.SellPrice
                                                               }).ToListAsync();
            return featuredExams;
        }
    }
}
