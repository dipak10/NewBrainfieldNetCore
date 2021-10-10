using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Repositories.Interfaces
{
    public interface IStandardRepository 
    {
        int Add(tblStandard standard);

        Task<bool> Delete(int id);
    }
}
