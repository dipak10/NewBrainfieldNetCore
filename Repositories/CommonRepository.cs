using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly ApplicationContext _context;

        public CommonRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<tblChapters>> GetChapters()
        {
            return await _context.tblChapters.Include(x => x.Subjects).ThenInclude(x => x.Standards).ToListAsync();
        }

        public async Task<List<tblStandard>> GetStandards()
        {
            return await _context.tblStandard.OrderBy(x => x.StandardID).ToListAsync();
        }

        public async Task<List<tblSubject>> GetSubjects()
        {
            return await _context.tblSubject.Include(x => x.Standards).ToListAsync();
        }

        public async Task<List<tblExamMaster>> GetExams()
        {
            return await _context.tblExamMaster.ToListAsync();

        }
    }
}
