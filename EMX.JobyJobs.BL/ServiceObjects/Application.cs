using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.ServiceObjects
{
    /// <summary>
    /// Represents a job application.
    /// </summary>
    public class Application
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public Enums.ApplicationStatuses Status{ get;}

        public Application()
        {
            
        }

        public Application(application item)
        {
            
        }
    }
}
