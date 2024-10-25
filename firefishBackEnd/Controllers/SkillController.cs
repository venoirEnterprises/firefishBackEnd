using firefishBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace firefishBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class SkillController : Controller
    {
        private SkillsService skillsService = new();
        
        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll()
        {
            return Json(skillsService.GetSkills());
        }
    }
}
