using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface IStandardService
    {
        List<StandardDTO> Get();

        int AddStandard(StandardDTO standard);

        Task<bool> DeleteStandard(int id);
    }
}
