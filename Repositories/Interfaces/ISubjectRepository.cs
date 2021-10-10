using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Repositories.Interfaces
{
    public interface ISubjectRepository
    {
        int Add(tblSubject subject);

        Task<bool> Delete(int id);
    }
}
