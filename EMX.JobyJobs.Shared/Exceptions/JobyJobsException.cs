using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.Shared.Exceptions
{
    /// <summary>
    /// Represents a joby-jobs internal exception.
    /// </summary>
    public class JobyJobsException : Exception
    {
        public JobyJobsException() : base()
        {

        }

        public JobyJobsException(string msg) : base(msg)
        {

        }
        public JobyJobsException(string msg, Exception innerException) : base(msg, innerException)
        {

        }
    }
}
