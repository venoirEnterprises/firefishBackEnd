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

        [HttpGet]
        [Route("Get")]
        public JsonResult Get(int ID)
        {
            return Json(candidateService.GetCandidate(ID));
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult UpdateCandidate(Candidate candidate)
        {
            return Ok(candidateService.UpdateCandidate(candidate));
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult CreateCandidate(Candidate candidate)
        {      
            return Ok(candidateService.CreateCandidate(candidate));
        }
    }
}
