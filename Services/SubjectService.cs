using AutoMapper;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Repositories.Interfaces;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        private IMapper _mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public int AddSubject(SubjectDTO subject)
        {
            var map = _mapper.Map<SubjectDTO, tblSubject>(subject);
            var response = _subjectRepository.Add(map);
            return response;
        }

        public Task<bool> DeleteSubject(int id)
        {
            throw new NotImplementedException();
        }

        public List<SubjectDTO> Get()
        {
            throw new NotImplementedException();
        }
    }
}
