using NewBrainfieldNetCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface ISubjectService
    {
        List<SubjectDTO> Get();
        int AddSubject(SubjectDTO subject);

        Task<bool> DeleteSubject(int id);
    }
}
