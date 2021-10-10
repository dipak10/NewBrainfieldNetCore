using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly ApplicationContext _context;

        public SubjectRepository(ApplicationContext context)
        {
            _context = context;
        }

        public int Add(tblSubject subject)
        {
            _context.tblSubject.Add(subject);
            _context.SaveChanges();
            return subject.SubjectID;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
