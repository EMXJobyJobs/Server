using EMX.JobyJobs.BL.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all requests around professions, sub-professions, fields and so on.
  /// </summary>
  [Authorize(Roles = "Seeker,Employer")]
  public class ProfessionsController : Controller
  {
    private IProfessionsBL _professionsBL;

    public ProfessionsController(IProfessionsBL professionsBL)
    {
      this._professionsBL = professionsBL;
    }

    // GET: /<controller>/
    /// <summary>
    /// not relevant
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
      return View();
    }


    //Seekers Point-Of-View:::

    public IActionResult GetProfessionsHierarchy()
    {
      var data = _professionsBL.GetProfessionsHierarchy();
      return Json(data);
    }


    //Employers Point-Of-View:::

  }
}
