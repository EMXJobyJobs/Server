using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.DAL.Models;

namespace EMX.JobyJobs.BL.ServiceObjects
{
    public class Interview
    {
        public int InterviewId { get; set; }
        public int CompanyId { get; set; }
        public int WorkerId { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Location inside the company.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// General notes.
        /// </summary>
        public string Notes { get; set; }

        public bool IsCancelled { get; set; }
        public string CancelReason { get; set; }
        public Interview(interview item)
        {
        }
    }
}
