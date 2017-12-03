using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.ServiceObjects;
using EMX.JobyJobs.DAL.Models;

namespace EMX.JobyJobs.BL.Business
{
    /// <summary>
    /// Handles all business-logic activities around position, searches, search tags, position precedence scheme and so on.
    /// also contains general methods for the application.
    /// </summary>
    public class PositionsBL
    {
        public static FeaturedPositionsList GetFeaturedPositions()
        {
            using (var db = new JobyJobsDB2())
            {
                var items = db.positions.Take(10).AsEnumerable()
                    .Select(ServiceObjectsExtensions.ToSvc)
                    /*.OrderBy(item => item.Precedence)*/.ToList();
                return new FeaturedPositionsList(items);    //todo. add random
            }
        }

        public static PositionsStats GetPositionsStats()
        {
            return new PositionsStats();
        }

        public static List<Field> GetFields()
        {
            using (var db = new JobyJobsDB2())
            {
                var items = db.fields.Take(10).AsEnumerable()
                    .Select(ServiceObjectsExtensions.ToSvc).ToList();

                return items;
            }
        }

        public static List<Position> GetLastActivePositions()
        {
            return new List<Position>();
        }
    }
}
