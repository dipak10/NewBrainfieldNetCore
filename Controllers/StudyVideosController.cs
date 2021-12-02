using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewBrainfieldNetCore.Services.Interfaces;
using System.Threading.Tasks;

namespace NewBrainfieldNetCore.Controllers
{
    [AllowAnonymous]
    public class StudyVideosController : Controller
    {

        private readonly IStudyVideosServices _studyVideosServices;
        
        public StudyVideosController(IStudyVideosServices studyVideosServices)
        {
            _studyVideosServices = studyVideosServices;
        }

        [HttpGet]        
        public async Task<ActionResult> Index()
        {
            var videos = await _studyVideosServices.GetFreeVideos();
            var model = new Viewmodels.ListStudyVideosViewModel()
            {
                Videos = videos,
            };
            _studyVideosServices.Dispose();
            return View(model);
        }
    }
}
