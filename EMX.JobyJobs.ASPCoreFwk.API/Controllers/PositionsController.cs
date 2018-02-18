using System;
using EMX.JobyJobs.ASPCoreFwk.API.Helpers;
using EMX.JobyJobs.ASPCoreFwk.API.Models;
using EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all requests around position, searches, search tags, position precedence scheme and so on.
  /// </summary>
  [Authorize]
  public class PositionsController : Controller
  {
    private IPositionsBL _positionsBL;

    public PositionsController(IPositionsBL positionsBL)
    {
      _positionsBL = positionsBL;
    }

    //Seeker Point-Of-View:::
    public IActionResult Index()
    {
      return View();
    }

    /// <summary>
    /// Seeker side (both): Gets the position of the given id. 
    /// </summary>
    /// <param name="positionId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Seeker,Employer")]
    public IActionResult GetPosition(int positionId)
    {
      return Json(_positionsBL.GetPosition(positionId));
    }

    [Authorize(Roles = "Seeker,Employer")]
    public IActionResult GetPositionShareableLink(Guid positionUID)
    {
      return Content(_positionsBL.GetPositionShareableLink(positionUID));
    }

    /// <summary>
    /// Seeker side: Searches for all positions matching the specified criteria.
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult SearchPosition(PositionsSearchCriteria criteria)
    {
      return Json(_positionsBL.SearchPosition(criteria));
    }


    //Employer Point-Of-View ::::


    /// <summary>
    /// Employer side: Posts a new position of the given information.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult PostPositionAsDraft(PostPositionServiceRequest data)
    {
      _positionsBL.PostPosition(data.ToBusiness(), false);
      return Ok();
    }

    /// <summary>
    /// Employer side: Posts a new position of the given information.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult PostPosition(PostPositionServiceRequest data)
    {
      _positionsBL.PostPosition(data.ToBusiness(), true);
      return Ok();
    }


    /// <summary>
    /// Employer side: Gets all positions of the given employer.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetAllPositions()
    {
      int employerId = this.GetIdentityEmployerId();
      return Json(_positionsBL.GetPositionsByStatus(employerId, Enums.PositionStatuses.All));
    }

    /// <summary>
    /// Employer side: Gets all positions of the given employer, filtered by status.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetPositionsByStatus(GetPositionsByStatusServiceRequest criteria)
    {
      return Json(_positionsBL.GetPositionsByStatus(this.GetIdentityEmployerId(), criteria.Status));
    }

    /// <summary>
    /// Employer side: Sets the position status to the specified value.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult SetPositionStatus(SetPositionStatusServiceRequest data)
    {
      switch (data.NewStatus)
      {
        case Enums.PositionStatuses.OnAir:
          _positionsBL.ExposePosition(data.PositionId);
          break;
        case Enums.PositionStatuses.Frozen:
          _positionsBL.FreezePosition(data.PositionId);
          break;
        case Enums.PositionStatuses.Unfrozen:
          _positionsBL.UnfreezePosition(data.PositionId);
          break;
        case Enums.PositionStatuses.Archived:
          _positionsBL.ArchivePosition(data.PositionId);
          break;
        case Enums.PositionStatuses.Unarchived:
          _positionsBL.UnarchivePosition(data.PositionId);
          break;
        case Enums.PositionStatuses.All:
        case Enums.PositionStatuses.Draft:
        case Enums.PositionStatuses.Unspecified:
        default:
          throw new ArgumentOutOfRangeException();
      }
      //_positionsBL.SetPositionStatus(data.EmployerId, data.NewStatus);
      return Ok();
    }


    //Admin Point-Of-View:::
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult SetPositionRank(int positionId, Enums.PositionRanks rank)
    {
      _positionsBL.SetPositionRank(positionId, rank);
      return Ok();
    }
  }
}