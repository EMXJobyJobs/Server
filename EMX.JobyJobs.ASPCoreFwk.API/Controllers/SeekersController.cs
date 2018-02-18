using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.ASPCoreFwk.API.Helpers;
using EMX.JobyJobs.ASPCoreFwk.API.Models;
using EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.Shared.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all requests around seekers. (Not including Account Management)
  /// </summary>
  [Authorize]
  public class SeekersController : Controller
  {
    private ISeekersBL _seekersBL;
    private IApplicationsBL _applicationsBL;
    private IGeneralManager _generalManager;
    private IConfiguration _config;
    private General _configGeneral;

    public SeekersController(IConfiguration config, ISeekersBL seekersBL, IApplicationsBL applicationsBL, IGeneralManager generalManager)
    {
      this._config = config;
      this._configGeneral = config.Get<AppSettings>().General;
      this._seekersBL = seekersBL;
      this._applicationsBL = applicationsBL;
      this._generalManager = generalManager;
    }


    //Seekers Point-Of-View:::
    public IActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// Seeker side: Updates the seeker of the given information.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult UpdateSeeker(UpdateSeekerServiceRequest data)
    {
      _seekersBL.UpdateSeeker(data.SeekerId, data.ToBusiness());
      return Ok();
    }

    /// <summary>
    /// Seeker side (both): Returns the seeker of the given id.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Seeker,Employer")]
    public IActionResult GetSeeker(int seekerId)
    {
      return Json(_seekersBL.GetSeeker(seekerId));
    }
    /// <summary>
    /// Seeker side: Updates the work state of the seeker.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult UpdateSeekerWorkState(UpdateSeekerWorkStateServiceRequest data)
    {
      _seekersBL.UpdateSeekerWorkState(data.SeekerId, data.NewState);
      return Ok();
    }









    //Employers Point-Of-View:::
    /// <summary>
    /// Employer side: Searches for all job seekers matching the specified criteria.
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult SearchSeeker(SeekersSearchCriteria criteria)
    {
      return Json(_seekersBL.SearchSeeker(criteria));
    }

    /// <summary>
    /// Downloads the seeker's avatar image.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult DownloadSeekerAvatarFile(int seekerId)
    {
      return this.JpegFile(Path.Combine(_configGeneral.SeekerAvatarsUploadFolder, _seekersBL.GetSeeker(seekerId).Avatar), false);
    }
    /// <summary>
    /// Downloads an seeker cv file.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult DownloadSeekerCVFile(int seekerId)
    {
      return this.WordFile(Path.Combine(_configGeneral.SeekerCVsUploadFolder, _seekersBL.GetSeeker(seekerId).CV_File), false);
    }

    //private string fileName { get; set; }
    //public IActionResult createFile()
    //{
    //  string wwwrootPath = _hostingEnvironment.WebRootPath;
    //  fileName = @"Employees.xlsx";
    //  FileInfo file = new FileInfo(Path.Combine(wwwrootPath, fileName));
    //  return downloadFile(wwwrootPath);
    //}
    //public FileResult downloadFile(string filePath)
    //{
    //  IFileProvider provider = new PhysicalFileProvider(filePath);
    //  IFileInfo fileInfo = provider.GetFileInfo(fileName);
    //  var readStream = fileInfo.CreateReadStream();
    //  var mimeType = "application/vnd.ms-excel";
    //  return File(readStream, mimeType, fileName);
    //}
  }
}