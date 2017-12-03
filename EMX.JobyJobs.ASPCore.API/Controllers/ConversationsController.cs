using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.ServiceObjects;
using EMX.JobyJobs.ProxyServices.Managers;
using EMX.JobyJobs.Shared.ServiceObjects;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMX.JobyJobs.ASPCore.API.Controllers
{
    /// <summary>
    /// Handles all requests around conversations (chat messages) etc.
    /// </summary>
    public class ConversationsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public void PostMessage(ChatMessage message)
        {
            //Save in db (async).
            ConversationsBL.AddMessageAsync(message);

            //Peer-to-peer (push).
            ChatManager chatManager = new ChatManager();
            chatManager.PostMessage(message);
        }
    }
}
