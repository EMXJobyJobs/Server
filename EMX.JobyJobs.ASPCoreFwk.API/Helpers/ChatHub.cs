using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace EMX.JobyJobs.ASPCoreFwk.API.Helpers
{
  /// <summary>
  /// A signal-r chat hub for conversations between seekers and employers.
  /// </summary>
  public class ChatHub : Hub
  {
    public async Task Send(string nick, string message)
    {
      await Clients.All.InvokeAsync("Send", nick, message);
    }
  }
}
