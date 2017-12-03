using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMX.JobyJobs.ASPCore.API.Managers
{
    /// <summary>
    /// Signals notifications for events.
    /// </summary>
    public class EventNotificationManager
    {
        public event EventHandler CompanyWorkerMatch;

        /// <summary>
        /// Internal: Raises the company-worker-match event.
        /// </summary>
        public void OnCompanyWorkerMatch()
        {
            
        }
    }
}
