using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.ServiceObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all requests around professions, sub-professions, fields and so on.
    /// </summary>
    public class ProfessionsController : Controller
    {
        // GET: /<controller>/
        /// <summary>
        /// not relevant
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFieldsHierarch()
        {
            var data = ProfessionsBL.GetFProfessionHierarchy();
            return Json(data);
        }

    }
}
