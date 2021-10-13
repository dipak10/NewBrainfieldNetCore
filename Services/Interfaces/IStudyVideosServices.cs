using NewBrainfieldNetCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Services.Interfaces
{
    public interface IStudyVideosServices
    {
        Task<List<tblVideos>> GetVideos();
        Task<List<tblVideos>> GetFreeVideos();
        Task<tblVideos> AddVideoes(tblVideos video);
        void DeleteVideo(int id);       
        Task<bool> ChangeExcemption(int id);
        void Dispose();
    }
}
