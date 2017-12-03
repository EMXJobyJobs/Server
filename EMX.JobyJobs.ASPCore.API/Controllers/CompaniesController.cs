using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.ServiceObjects;
using EMX.JobyJobs.DAL.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all requests around companies, company persons etc.
    /// </summary>
    public class CompaniesController : Controller
    {
        //Worker Point-Of-View:::
        //CompanyInfo page:
        // GET: /<controller>/
        /// <summary>
        /// not relevant
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }


        //Company Point-Of-View:::
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                CompaniesBL.SaveCompany(company);
                return RedirectToAction("Index");
            }
            //ViewBag.company_id = new SelectList(db.companies, "company_id", "name", worker.company_id);
            //ViewBag.identity_user_id = new SelectList(db.users, "Id", "Email", worker.identity_user_id);
            return View(company);
        }
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                CompaniesBL.SaveCompany(company);
                return RedirectToAction("Index");
            }
            //ViewBag.company_id = new SelectList(db.companies, "company_id", "name", worker.company_id);
            //ViewBag.identity_user_id = new SelectList(db.users, "Id", "Email", worker.identity_user_id);
            return View(company);
        }
    }
}
