using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMX.JobyJobs.ASPCoreFwk.API.Helpers
{
    public class SiteEnums
    {
      public enum ClientUIMode
      {
        Unspecified = 0,
        MVC = 1,  //MVC-no template
        Template = 2,  //MVC-Seeker template
        Angular = 3    //WebAPI-Angular
      }
  }
}
