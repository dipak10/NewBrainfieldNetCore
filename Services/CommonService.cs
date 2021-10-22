using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AspNetUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonService(ICommonRepository commonRepository, IMapper mapper,
            UserManager<AspNetUser> userManager, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _commonRepository = commonRepository;
            _mapper = mapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<tblChapters>> GetChapters()
        {
            return await _commonRepository.GetChapters();
        }

        public async Task<List<StandardDTO>> GetStandards()
        {
            var standards = await _commonRepository.GetStandards();
            return _mapper.Map<List<tblStandard>, List<StandardDTO>>(standards);
        }

        public async Task<List<tblSubject>> GetSubjects()
        {
            return await _commonRepository.GetSubjects();
        }

        public async Task<List<tblExamMaster>> GetExams()
        {
            return await _commonRepository.GetExams();
        }

        public async Task<AspNetUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }
    }
}
