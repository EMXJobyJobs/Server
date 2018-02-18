using System;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using EMX.JobyJobs.ASPCoreFwk.API.Helpers;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Helpers;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SiteEnums = EMX.JobyJobs.ASPCoreFwk.API.Helpers.SiteEnums;
using Microsoft.Extensions.DependencyInjection;

namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{



  /// <summary>
  /// Handles all general-purpose requests.
  /// </summary>
  [AllowAnonymous]
  public class HomeController : Controller
  {
    private static readonly ILog m_logger = LogManager.GetLogger(typeof(HomeController));

    private IServiceProvider _sp;
    private IConfiguration _config;
    private General _configGeneral;


    //Seeker (General) Point-Of-View:::
    public HomeController(IConfiguration config, IServiceProvider sp)
    {
      this._config = config;
      this._configGeneral = config.Get<AppSettings>().General;
      this._sp = sp;
    }


    // GET: /<controller>/
    public IActionResult Index()
    {
      SiteEnums.ClientUIMode clientMode = _configGeneral.ClientUIMode;
      var viewName = EnumsHelper.GetTriEnumString(clientMode, "Index", "IndexMVC", "IndexTempl");
      return View(viewName);
    }

    //public IActionResult Contact()
    //{
    //  ViewBag.Message = "צור קשר :";

    //  return View();
    //}

    public IActionResult Error()
    {
      ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
      return View();
    }

    public IActionResult Test()
    {
      m_logger.Error("Test: Hello from JobyJobs app !!");
      return Content("Hello from JobyJobs app !!");
    }

    public IActionResult Test2()
    {
      return null;
      //var seeker = new seeker(1, Guid.NewGuid().ToString(), "Ori", "Ahodur", "0544488847", "0349882992", "sds@dsds.com",
      //          DateTime.Now, DateTime.Now.AddDays(365 * 18), (int)Enums.Gender.Male, true, DateTime.Now);
      //var res = Content(TestRun(_mapper.Map<seeker, Seeker>(seeker)));
      //return res;
    }

    private string TestRun(Seeker obj)
    {
      var res = JsonConvert.SerializeObject(obj);
      return res;
    }

    ///// <summary>
    ///// Returns the key-word to value mapping for the ui side.
    ///// </summary>
    ///// <returns></returns>
    //public IActionResult GetUIKeyMap()
    //{
    //  return View();
    //}

    //hot links:




    //Employers Point-Of-View:::

  }
}
