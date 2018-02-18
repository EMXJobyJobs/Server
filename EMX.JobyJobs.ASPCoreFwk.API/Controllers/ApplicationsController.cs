using System;
using System.IO;
using EMX.JobyJobs.ASPCoreFwk.API.Helpers;
using EMX.JobyJobs.ASPCoreFwk.API.Models;
using EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all requests around applications, interviews etc.
  /// </summary>
  [Authorize]
  public class ApplicationsController : Controller
  {
    private IServiceProvider _sp;
    private ISeekersBL _seekersBL;
    private IApplicationsBL _applicationsBL;
    private IGeneralManager _generalManager;
    private IConfiguration _config;
    private General _configGeneral;
    private IReactionsBL _reactionsBL;

    public ApplicationsController(IConfiguration config, IServiceProvider sp, ISeekersBL seekersBL, IApplicationsBL applicationsBL, IGeneralManager generalManager, IReactionsBL reactionsBL)
    {
      this._config = config;
      this._configGeneral = config.Get<AppSettings>().General;
      this._sp = sp;
      this._seekersBL = seekersBL;
      this._applicationsBL = applicationsBL;
      this._generalManager = generalManager;
      this._reactionsBL = reactionsBL;
    }

    //Seeker Point-Of-View:::
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult ApplyForPosition(ApplyForPositionServiceRequest data)
    {
      _sp.GetService<IApplicationsBL>().ApplyForPosition(data.SeekerId, data.PositionId, data.PositionUID);
      return Ok();
    }

    [Authorize(Roles = "Seeker")]
    public IActionResult GetMyApplications(int seekerId)
    {
      var data = _sp.GetService<IApplicationsBL>().GetMyApplications(seekerId);
      return Json(data);
    }


    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult DropApplication(DropApplicationServiceRequest data)
    {
      _sp.GetService<IApplicationsBL>().DropApplication(data.SeekerId, data.ApplicationId);
      return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult DropAllApplication(DropAllApplicationsServiceRequest data)
    {
      _sp.GetService<IApplicationsBL>().DropAllApplications(data.SeekerId);
      return Ok();
    }
    [Authorize(Roles = "Seeker")]
    public IActionResult GetInterviewByUID(Guid interviewUID)
    {
      _sp.GetService<IApplicationsBL>().GetInterviewByUID(interviewUID);
      return Ok();
    }
    [Authorize(Roles = "Seeker")]
    public IActionResult ApproveInterview(RespondInterviewServiceRequest data)
    {
      _sp.GetService<IApplicationsBL>().ApproveInterview(data.InterviewId);
      return Ok();
    }
    [Authorize(Roles = "Seeker")]
    public IActionResult RejectInterview(RespondInterviewServiceRequest data)
    {
      _sp.GetService<IApplicationsBL>().RejectInterview(data.InterviewId, data.RejectReason);
      return Ok();
    }







    //Employer Point-Of-View:::
    [Authorize(Roles = "Employer")]
    public IActionResult GetPositionApplications(GetPositionApplicationsServiceRequest data)
    {
      return Json(_sp.GetService<IApplicationsBL>().GetPositionApplications(data.EmployerId, data.PositionId));
    }
    [Authorize(Roles = "Employer")]
    public IActionResult GetAllApplications(GetAllApplicationsServiceRequest data)
    {
      return Json(_sp.GetService<IApplicationsBL>().GetAllApplications(data.EmployerId));
    }

    [Authorize(Roles = "Employer")]
    public IActionResult GetApplication(int applicationId)
    {
      return Json(_sp.GetService<IApplicationsBL>().GetApplication(applicationId));
    }


    /// <summary>
    /// Note: I Liked. double method with ReactionsController.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetLikingCandidates(GetLikingCandidatesServiceRequest data)
    {
      return Json(_reactionsBL.GetLikedCandidates(data.EmployerPersonId, data.PositionId));
    }

    /// <summary>
    /// Note: Liked me. double method with ReactionsController.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetLikingCandidates(int positionId)
    {
      return Json(_reactionsBL.GetLikingCandidates(positionId));
    }

    /// <summary>
    /// Note: Double method with ReactionsController.
    /// </summary>
    /// <param name="positionId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetMatchedCandidates(GetMatchedCandidatesServiceRequest data)
    {
      return Json(_reactionsBL.GetEmployerPersonMatches(data.EmployerPersonId, data.PositionId));
    }
    /// <summary>
    /// Returns all candidates that were hired for the given position.
    /// </summary>
    /// <param name="positionId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetHiredCandidates(int positionId)
    {
      return Json(_applicationsBL.GetHiredCandidates(positionId));
    }

    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult SetInterview(SetInterviewServiceRequest interview)
    {
      _sp.GetService<IApplicationsBL>().SetInterview(interview.ToBusiness());
      return Ok();
    }
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult CancelInterview(CancelInterviewServiceRequest data)
    {
      _sp.GetService<IApplicationsBL>().CancelInterview(data.SeekerId, data.EmployerId, data.CancelReason);
      return Ok();
    }
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult UpdateInterview(UpdateInterviewServiceRequest data)
    {
      _sp.GetService<IApplicationsBL>().UpdateInterview(data.InterviewId, data.DueDate, data.Location, data.Notes);
      return Ok();
    }
    [Authorize(Roles = "Employer")]
    public IActionResult UpdateApplicationStatus(UpdateApplicationStatusServiceRequest model)
    {
      _sp.GetService<IApplicationsBL>().UpdateApplicationStatus(model.ApplicationId, model.NewStatus);
      return Ok();
    }

    /// <summary>
    /// Downloads a candidate(seeker) cv file.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult DownloadCandidateCVFile(int applicationId)
    {
      var cvFilePath = _applicationsBL.GetCandidateCVFile(applicationId);
      return this.WordFile(Path.Combine(_configGeneral.SeekerCVsUploadFolder, cvFilePath), false);
    }

    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult SetInterviewNotes(SetInterviewNotesServiceRequest data)
    {
      _applicationsBL.SetInterviewNotes(data.InterviewId, data.Notes);
      return Ok();
    }
    [Authorize(Roles = "Employer")]
    public IActionResult GetPositionInterviews(int applicationId)
    {
      return Json(_applicationsBL.GetPositionInterviews(applicationId));
    }



    
  }
}