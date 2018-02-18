using EMX.JobyJobs.ASPCoreFwk.API.Models;
using EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels;
using EMX.JobyJobs.BL.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all business-logic activities around reactions, that is employer-likes-seeker, seeker-likes-employer and mutual matches.
  /// </summary>
  [Authorize]
  public class ReactionsController : Controller
  {
    private IReactionsBL _reactionsBL;

    public ReactionsController(IReactionsBL reactionsBL)
    {
      this._reactionsBL = reactionsBL;
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

    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult SeekerLikesPosition(SeekerLikesPositionServiceRequest data)
    {
      _reactionsBL.SetSeekerLikesPosition(data.SeekerId, data.PositionId);
      return Ok();
    }

    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult SeekerUnlikesPosition(SeekerLikesPositionServiceRequest data)
    {
      _reactionsBL.SetSeekerUnlikesPosition(data.SeekerId, data.PositionId);
      return Ok();
    }

    [Authorize(Roles = "Seeker")]
    public IActionResult GetSeekerMatches(int seekerId)
    {
      return Json(_reactionsBL.GetSeekerMatches(seekerId));
    }

    /// <summary>
    /// Returns all the positions that were liked by the current seeker.
    /// Note: I Liked
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Seeker")]
    public IActionResult GetLikedPositions(int seekerId)
    {
      return Json(_reactionsBL.GetLikedPositions(seekerId));
    }

    /// <summary>
    /// Returns all the employers that were liked by the current seeker.
    /// Note: I Liked
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    public IActionResult GetLikedEmployers(int seekerId)
    {
      return Json(_reactionsBL.GetLikedEmployers(seekerId));
    }

    /// <summary>
    /// Returns all the positions who's employers had liked the current seeker.
    /// Note: Liked me.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Seeker")]
    public IActionResult GetLikingPositions(int seekerId)
    {
      return Json(_reactionsBL.GetLikingPositions(seekerId));
    }


    /// <summary>
    /// Returns all the employers that had liked the current seeker.
    /// Note: Liked me.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Seeker")]
    public IActionResult GetLikingEmployers(int seekerId)
    {
      return Json(_reactionsBL.GetLikingEmployers(seekerId));
    }












    //Employer Point-Of-View:::
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult EmployerLikesSeekerAndPosition(EmployerLikesSeekerAndPositionServiceRequest data)
    {
      _reactionsBL.SetEmployerPersonLikesSeekerAndPosition(data.EmployerPersonId, data.SeekerId, data.PositionId);
      return Ok();
    }
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult EmployerUnlikesSeekerAndPosition(EmployerLikesSeekerAndPositionServiceRequest data)
    {
      _reactionsBL.SetEmployerPersonUnlikesSeekerAndPosition(data.EmployerPersonId, data.SeekerId, data.PositionId);
      return Ok();
    }

    /// <summary>
    /// Note: I Liked.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetLikingCandidates(GetLikingCandidatesServiceRequest data)
    {
      return Json(_reactionsBL.GetLikedCandidates(data.EmployerPersonId, data.PositionId));
    }

    /// <summary>
    /// Note: Liked me.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetLikingCandidates(int positionId)
    {
      return Json(_reactionsBL.GetLikingCandidates(positionId));
    }

    [Authorize(Roles = "Employer")]
    public IActionResult GetEmployerPersonMatches(GetEmployerPersonMatchesServiceRequest data)
    {
      return Json(_reactionsBL.GetEmployerPersonMatches(data.EmployerPersonId, data.PositionId));
    }

    ///// <summary>
    ///// Returns all the seekers that had liked the current employer.
    ///// </summary>
    ///// <param name="employerId"></param>
    ///// <returns></returns>
    //public IActionResult GetAllEmployerLikes(int employerId) => Json(_reactionsBL.GetAllEmployerLikes(employerId));

    ///// <summary>
    ///// Returns all the seekers that are liked by the current employer.
    ///// </summary>
    ///// <param name="employerId"></param>
    ///// <returns></returns>
    //public IActionResult GetAllEmployerInitiatedLikes(int employerId) => Json(_reactionsBL.GetAllEmployerInitiatedLikes(employerId));

  }
}