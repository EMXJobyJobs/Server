using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.BL.ServiceObjects
{
    /// <summary>
    /// Holds position statistics results.
    /// </summary>
    public class PositionsStats
    {
        public int NumOfCandidates { get; set; }
        public int NumOfEmployers { get; set; }
        public int NumOfPositions { get; set; }
    }
}
