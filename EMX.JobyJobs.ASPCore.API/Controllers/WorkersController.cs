using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all requests around workers.
    /// </summary>
    public class WorkersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}