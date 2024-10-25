using firefishBackEnd.Models;
using firefishBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace firefishBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CandidateController : Controller
    {
        private readonly CandidateService candidateService = new();

        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll()
        {
            return Json(candidateService.GetCandidates());
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult CreateCandidate(Candidate candidate)
        {      
            return Ok(candidateService.CreateCandidate(candidate));
        }
    }
}
