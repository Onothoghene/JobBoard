using JobBoard.Handlers;
using JobBoard.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JobBoard.Controllers
{
    public class JobController : BaseController
    {
        readonly IJob _job;
        RegionReader _regionReader = new RegionReader();
        readonly IHttpContextAccessor _httpContext;

        public JobController(IJob job , IHttpContextAccessor httpContext) : base(httpContext)
        {
            _job = job;
            _httpContext = httpContext;
        }

        [Route("job-list")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("job")]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult CreatOrEditJob()
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        [Route("job/details")]
        ///[Route("job/details/{title}")]
        public IActionResult Details(int jobId)
        {
            return View();
        }
        
        //[Route("recruiter/jobs")]
        //[Authorize]
        //public IActionResult RecruiterJobs()
        //{
        //    try
        //    {

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


    }
}
