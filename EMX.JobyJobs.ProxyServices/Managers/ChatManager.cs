using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.ProxyServices.Managers
{
  public interface IChatManager : IDisposable
  {
    void Start();
    void PostMessage(ChatMessage msg);
    void Stop();
  }

  public class ChatManager : IChatManager
  {
    private IChatManagerCaller _caller;

    public ChatManager(/*IChatManagerCaller caller*/)
    {
      //this._caller = caller;
    }
    public void Start()
    { }

    public void PostMessage(ChatMessage msg)
    {

    }
    public void Stop()
    {

    }

    public void Dispose()
    {
      this._caller = null;
    }

  }

  public interface IChatManagerCaller
  {
    /// <summary>
    /// Occurs when a new message was received.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="data"></param>
    void OnMessageReceived(object sender, GenericEventArgs<ChatMessage> data);
  }
}
