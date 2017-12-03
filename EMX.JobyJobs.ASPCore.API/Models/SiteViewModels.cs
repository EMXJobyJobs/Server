using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.ASPCore.API.Models
{
    //Worker Point-Of-View:::
    public class WorkerContactUsViewModel
    {
        public Enums.ContactUsSubject Subject { get; set; }
        public string Content { get; set; }
    }


    //Company Point-Of-View:::
    public class CompanyContactUsViewModel
    {
        public Enums.ContactUsSubject Subject { get; set; }
        public string Content { get; set; }
    }


}
