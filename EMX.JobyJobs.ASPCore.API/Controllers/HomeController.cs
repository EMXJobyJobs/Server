using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all general-purpose requests.
    /// </summary>
    public class HomeController : Controller
    {

        public IConfiguration Configuration { get; set; }

        //Workers Point-Of-View:::
        //MainPage:
        public HomeController(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            string clientMode = Configuration["ClientMode"];
            bool isMVC = clientMode.ToLower() == "MVC".ToLower();

            if (isMVC)
            {
                return View("IndexMVC");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "צור קשר :";

            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }


        //Company Point-Of-View:::
        [ActionName("CompanyIndex")]
        public ActionResult CompanyIndex()
        {
            string clientMode = Configuration["ClientMode"];
            bool isMVC = clientMode.ToLower() == "MVC".ToLower();

            if (isMVC)
            {
                return View("IndexMVC");
            }
            else
            {
                return View();
            }
        }


    }
}
