using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Repositories.Interfaces
{
    public interface ICommonRepository
    {
        Task<List<tblStandard>> GetStandards();

        Task<List<tblSubject>> GetSubjects();

    }
}
