using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Repositories.Interfaces;
using NewBrainfieldNetCore.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Repositories
{
    public class StandardRepository : IStandardRepository
    {
        private readonly ApplicationContext _context;

        public StandardRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(tblStandard standard)
        {
            var add = _context.tblStandard.Add(standard);
            _context.SaveChanges();
            return standard.StandardID;            
        }

        public async Task<bool> Delete(int id)
        {
            var data = _context.tblStandard.Where(x=>x.StandardID == id).FirstOrDefault();
            if(data != null)
            {
                _context.tblStandard.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
