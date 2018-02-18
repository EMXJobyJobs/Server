using EMX.JobyJobs.ASPCoreFwk.API.Models;
using EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Exceptions;
using EMX.JobyJobs.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all requests around employers, employer persons etc. (Not including Account Management)
  /// </summary>
  [Authorize]
  public class EmployersController : Controller
  {
    private IEmployersBL _employersBL;

    public EmployersController(IEmployersBL employersBL)
    {
      this._employersBL = employersBL;
    }



    //Seeker (General) Point-Of-View:::
    /// <summary>
    /// not relevant
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
      return View();
    }

    //[Authorize(Roles = "Employer")]
    //public IActionResult Details(int id)
    //{
    //  return View();
    //}

   




    //Employer Point-Of-View:::
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult UpdateEmployer(UpdateEmployerServiceRequest data)
    {
      _employersBL.UpdateEmployer(data.EmployerId, data.ToBusiness());
      return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult UpdateEmployerPerson(UpdateEmployerPersonServiceRequest data)
    {
      _employersBL.UpdateEmployerPerson(data.EmployerPersonId, data.ToBusiness());
      return Ok();
    }
    //public IActionResult Create()
    //{
    //  return View();
    //}

    //[HttpPost]
    //public IActionResult Create(Employer employer)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        EmployersBL.SaveEmployer(employer);
    //        return RedirectToAction("Index");
    //    }
    //    //ViewBag.company_id = new SelectList(db.companies, "company_id", "name", worker.company_id);
    //    //ViewBag.identity_user_id = new SelectList(db.users, "Id", "Email", worker.identity_user_id);
    //    return View(employer);
    //}
    //public IActionResult Edit(int id)
    //{
    //    return View();
    //}

    //[HttpPost]
    //public IActionResult Edit(Employer employer)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        EmployersBL.SaveEmployer(employer);
    //        return RedirectToAction("Index");
    //    }
    //    //ViewBag.company_id = new SelectList(db.companies, "company_id", "name", worker.company_id);
    //    //ViewBag.identity_user_id = new SelectList(db.users, "Id", "Email", worker.identity_user_id);
    //    return View(employer);
    //}
  }
}
