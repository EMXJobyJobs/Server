using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace EMX.JobyJobs.ASPCoreFwk.API.Helpers
{
  /// <summary>
  /// A signal-r chat hub for notifications to clients.
  /// </summary>
  public class NotificationsHub : Hub
  {
    public void NotifyAll(string title, string message, string alertType)
    {
      Clients.All.InvokeAsync("displayNotification", title, message, alertType);
      //Clients.All. displayNotification(title, message, alertType);
    }
  }
}
