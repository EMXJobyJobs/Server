using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.ProxyServices.Managers
{

    public enum NotificationEngineParty
    {
        Unspecified = 0,
        Publisher = 1,
        Subscriber = 2
    }
    /// <summary>
    /// Allows notification between two modules in the application in a publisher/subscriber pattern.
    /// Handles both the publisher and subscriber of the event.
    /// </summary>
    public static class NotificationEngineManager
    {
    }
}
