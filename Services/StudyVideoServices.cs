using Microsoft.EntityFrameworkCore;
using NewBrainfieldNetCore.Data;
using NewBrainfieldNetCore.Entities;
using NewBrainfieldNetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services
{
    public class StudyVideoServices : IStudyVideosServices, IDisposable
    {
        private readonly ApplicationContext _examPortalEntities;

        public StudyVideoServices(ApplicationContext examPortalEntities)
        {
            _examPortalEntities = examPortalEntities;
        }

        public async Task<List<tblVideos>> GetFreeVideos()
        {
            return await _examPortalEntities.tblVideos.Where(x => x.IsFree == true).ToListAsync();
        }

        public async Task<tblVideos> AddVideoes(tblVideos video)
        {
            _examPortalEntities.tblVideos.Add(video);
            await _examPortalEntities.SaveChangesAsync();
            return video;
        }

        public async Task<List<tblVideos>> GetVideos()
        {
            return await _examPortalEntities.tblVideos.ToListAsync();
        }

        public void DeleteVideo(int id)
        {
            var data = _examPortalEntities.tblVideos.Where(x => x.VideosID == id).FirstOrDefault();
            if (data != null)
            {
                _examPortalEntities.tblVideos.Remove(data);
                _examPortalEntities.SaveChanges();
            }
        }
       
        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

        public async Task<bool> ChangeExcemption(int id)
        {
            //var data = await _examPortalEntities.Registers.Where(x => x.UserId == id).FirstAsync();
            //if (data.VideoPurchaseExcemption == true)
            //{
            //    data.VideoPurchaseExcemption = false;
            //}
            //else
            //{
            //    data.VideoPurchaseExcemption = true;
            //}
            //await _examPortalEntities.SaveChangesAsync();
            return true;
        }
    }
}
