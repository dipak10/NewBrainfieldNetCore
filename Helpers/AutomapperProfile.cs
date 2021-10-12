using AutoMapper;
using NewBrainfieldNetCore.Dto;
using NewBrainfieldNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<tblStandard, StandardDTO>();
            CreateMap<StandardDTO, tblStandard>();

            CreateMap<tblSubject, SubjectDTO>();
            CreateMap<SubjectDTO, StandardDTO>();
            CreateMap<List<tblSubject>, List<SubjectDTO>>();


            CreateMap<tblDownloadCategory, DownloadCategoryDTO>();
            CreateMap<DownloadCategoryDTO, tblDownloadCategory>();

            CreateMap<DownloadsDTO, tblDownloads>();
            CreateMap<tblDownloads, DownloadsDTO>();

            CreateMap<tblFaculties, FacultiesDTO>();
            CreateMap<List<tblFaculties>, List<FacultiesDTO>>();


        }
    }
}
