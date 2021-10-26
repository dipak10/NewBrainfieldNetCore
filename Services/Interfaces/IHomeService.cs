using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface IHomeService
    {
        Task<List<tblTestimonials>> GetTestimonials();

        Task<List<tblNews>> GetHomePageNews();      
    }
}
