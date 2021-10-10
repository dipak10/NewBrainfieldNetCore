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
    public class StandardService : IStandardService
    {
        private readonly IStandardRepository _standardRepository;

        private IMapper _mapper;

        public StandardService(IStandardRepository standardRepository, IMapper mapper)
        {
            _standardRepository = standardRepository;
            _mapper = mapper;
        }

        public int AddStandard(StandardDTO standard)
        {            
            var map = _mapper.Map<StandardDTO, tblStandard>(standard);
            var response = _standardRepository.Add(map);
            return response;            
        }

        public async Task<bool> DeleteStandard(int id)
        {
            var call = await _standardRepository.Delete(id);
            return call;
        }

        public List<StandardDTO> Get()
        {
            throw new NotImplementedException();
        }
    }
}
