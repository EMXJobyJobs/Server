using System;
using System.Collections.Generic;
using System.Drawing;
using EMX.JobyJobs.ASPCoreFwk.API.Models;
using EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.ProxyServices.Managers;
using EMX.JobyJobs.Shared.Helpers;
using EMX.JobyJobs.Shared.ServiceObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all requests around conversations (chat messages) etc.
  /// </summary>
  [Authorize]
  public class ConversationsController : Controller
  {
    private IServiceProvider _sp;

    public ConversationsController(IServiceProvider sp)
    {
      this._sp = sp;
    }

    // GET: /<controller>/
    public IActionResult Index()
    {
      return View();
    }

    //Seeker Point-Of-View:::
    [HttpPost]
    [Authorize(Roles = "Seeker")]
    public IActionResult SendMessageSeekerToEmployer(SendMessageSeekerToEmployerServiceRequest message)
    {
      _sp.GetService<IConversationsBL>().SendMessage(message.ToBusiness());
      return Ok();
    }

    [Authorize(Roles = "Seeker")]
    public IActionResult GetAllConversationsSeeker(int seekerId)
    {
      ConversationMessagesSeekerOverview res = _sp.GetService<IConversationsBL>().GetAllConversationsSeeker(seekerId);
      return Json(res);
    }


    [Authorize(Roles = "Seeker")]
    public IActionResult GetConversationMessagesSeeker(GetConversationMessagesSeekerServiceRequest data)
    {
      return Json(_sp.GetService<IConversationsBL>().GetConversationMessagesSeeker(data.SeekerId, data.EmployerPersonId));
    }

    /// <summary>
    /// Sets message as read.
    /// </summary>
    /// <param name="messageId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Seeker,Employer")]
    public IActionResult SetMessageAsRead(int messageId)
    {
      _sp.GetService<IConversationsBL>().SetMessageAsRead(messageId);
      return Ok();
    }

    /// <summary>
    /// Sets message as archived.
    /// </summary>
    /// <returns></returns>
    [Authorize(Roles = "Seeker,Employer")]
    public IActionResult ArchiveMessage(int messageId)
    {
      _sp.GetService<IConversationsBL>().ArchiveMessage(messageId);
      return Ok();
    }







    //Employer Point-Of-View:::
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult SendMessageEmployerToSeeker(SendMessageEmployerToSeekerServiceRequest message)
    {
      _sp.GetService<IConversationsBL>().SendMessage(message.ToBusiness());
      return Ok();
    }

    [Authorize(Roles = "Employer")]
    public IActionResult GetEmployerUnassignedMessages(GetEmployerUnassignedMessagesServiceRequest data)
    {
      return Json(_sp.GetService<IConversationsBL>().GetEmployerUnassignedMessages(data.EmployerId));
    }

    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult AssignEmployerMessage(AssignEmployerMessageServiceRequest data)
    {
      _sp.GetService<IConversationsBL>().AssignEmployerMessage(data.MessageId, data.EmployerPersonId);
      return Ok();
    }

    /// <summary>
    /// Returns the conversations list(list of all conversations).
    /// Note: does not include unassigned employer messages.
    /// </summary>
    /// <param name="employerPersonId"></param>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    public IActionResult GetAllConversationsEmployer(int employerPersonId)
    {
      return Json(_sp.GetService<IConversationsBL>().GetAllConversationsEmployer(employerPersonId));
    }


    [Authorize(Roles = "Employer")]
    public IActionResult GetConversationMessagesEmployer(GetConversationMessagesEmployerServiceRequest data)
    {
      return Json(_sp.GetService<IConversationsBL>().GetConversationMessagesEmployer(data.EmployerPersonId, data.SeekerId));
    }
  }
}
