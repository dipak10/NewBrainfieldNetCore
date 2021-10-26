using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Viewmodels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface IHomeService
    {
        Task<List<tblTestimonials>> GetTestimonials();

        Task<List<tblNews>> GetHomePageNews();

        Task<List<FeaturedExamViewModel>> GetFeaturedExams();
    }
}
