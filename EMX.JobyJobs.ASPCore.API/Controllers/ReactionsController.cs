using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all business-logic activities around reactions, that is company-likes-worker, worker-likes-company, and matches.
    /// </summary>
    public class ReactionsController : Controller
    {
        /// <summary>
        /// not relevant
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CompanyLikesWorker(int companyId, int workerId)
        {
            return View();
        }
        public IActionResult CompanyUnLikesWorker(int companyId, int workerId)
        {
            return View();
        }
        public IActionResult WorkerLikesCompany(int workerId, int companyId)
        {
            return View();
        }
        public IActionResult WorkerUnlikesCompany(int workerId, int companyId)
        {
            return View();
        }
    }
}