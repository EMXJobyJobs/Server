using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.BL.Helpers
{

  public interface ICryptographyHelper
  {
    string GenerateHash256(string password);
  }

}
