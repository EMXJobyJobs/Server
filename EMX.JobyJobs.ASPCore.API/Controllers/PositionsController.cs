using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.ServiceObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all requests around position, searches, search tags, position precedence scheme and so on.
    /// </summary>
    public class PositionsController : Controller
    {

        //Worker Point-Of-View:::
        //JobSearch page:
        /// <summary>
        /// Returns the featured positions, ordered by precedence. 
        /// </summary>
        /// <returns></returns>
        public IActionResult GetFeaturedPositions()
        {
            EMX.JobyJobs.BL.ServiceObjects.FeaturedPositionsList featuredList = PositionsBL.GetFeaturedPositions();
            return Content(JsonConvert.SerializeObject(featuredList));
        }

        public IActionResult Details(int id)
        {
            //PositionsBL.GetPosition(id);
            return View();
        }

        public IActionResult Search(int id)
        {
           //PositionsBL.GetPosition(id);
              return View();
        }

        //Companies Point-Of-View:::
        //PositionsPage:
        /// <summary>
        /// Returns the last active positions for the current company.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetLastActivePositions()
        {
            return Json(PositionsBL.GetLastActivePositions());
        }

        /// <summary>
        /// Returns all positions for the current company.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            int currentCompanyId;
            //var positions = PositionsBL.GetAllPositions(currentCompanyId);
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Position position)
        {
            if (ModelState.IsValid)
            {
                //CompaniesBL.SaveCompany(position);
                return RedirectToAction("Index");
            }
            //ViewBag.company_id = new SelectList(db.companies, "company_id", "name", worker.company_id);
            //ViewBag.identity_user_id = new SelectList(db.users, "Id", "Email", worker.identity_user_id);
            return View(position);
        }


    }
}