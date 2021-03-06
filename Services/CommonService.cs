using AutoMapper;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Helpers;
using NewBrainfieldNetCore.Repositories.Interfaces;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        private IMapper _mapper;


        public CommonService(ICommonRepository commonRepository, IMapper mapper)
        {
            _commonRepository = commonRepository;
            _mapper = mapper;

        }

        public async Task<List<StandardDTO>> GetStandards()
        {
            var standards = await _commonRepository.GetStandards();
            return _mapper.Map<List<tblStandard>, List<StandardDTO>>(standards);
        }

        public async Task<List<tblSubject>> GetSubjects()
        {
            
            List<tblSubject> data = await _commonRepository.GetSubjects();


            //test commit
            //tblSubject sa = null;           
            return data;
        }
    }
}
