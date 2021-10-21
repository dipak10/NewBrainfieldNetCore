using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface ICommonService
    {
        Task<List<StandardDTO>> GetStandards();

        Task<List<tblSubject>> GetSubjects();

        Task<List<tblChapters>> GetChapters();

        Task<List<tblExamMaster>> GetExams();
    }
}
