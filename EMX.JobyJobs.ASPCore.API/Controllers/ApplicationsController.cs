using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.ServiceObjects;
using Microsoft.AspNetCore.Mvc;

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all requests around applications, interviews etc.
    /// </summary>
    public class ApplicationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetInterview(Interview interview)
        {
            ApplicationsBL.SetInterview(interview);
            return Ok();
        }
        public IActionResult CancelInterview(int workerId, string cancelReason)
        {
            ApplicationsBL.CancelInterview(workerId, cancelReason);
            return Ok();
        }
    }
}