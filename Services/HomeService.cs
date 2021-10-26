using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationContext context;

        public HomeService(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task<List<tblNews>> GetHomePageNews()
        {
            return await context.tblNews.Where(x => x.IsActive == true).OrderByDescending(x => x.CreatedDate).ToListAsync();
        }

        public async Task<List<tblTestimonials>> GetTestimonials()
        {
            return await context.tblTestimonials.OrderByDescending(x => x.CreatedOn).ToListAsync();
        }
    }
}
