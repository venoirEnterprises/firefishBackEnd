using firefishBackEnd.Models;
using firefishBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace firefishBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class CandidateController : Controller
    {
        private CandidateService candidateService = new();

        [HttpGet]
        [Route("GetAll")]
        public JsonResult GetAll()
        {
            List<Candidate> candidates = candidateService.GetCandidates();
            return Json(candidates);
        }

        //// GET: CandidateController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: CandidateController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CandidateController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CandidateController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CandidateController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CandidateController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CandidateController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
