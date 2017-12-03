using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.ServiceObjects;

namespace EMX.JobyJobs.BL.Business
{
    /// <summary>
    /// Handles all business-logic activities around professions, sub-professions, fields and so on.
    /// </summary>
    public static class ProfessionsBL
    {
        public static ProfessionHierarchy GetFProfessionHierarchy()
        {
            return new ProfessionHierarchy();
        }
    }
}
