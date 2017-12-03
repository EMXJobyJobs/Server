using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.ServiceObjects;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Exceptions;
using EMX.JobyJobs.Shared.Helpers;

namespace EMX.JobyJobs.BL.Business
{
    /// <summary>
    /// Handles all business-logic activities around applications, interviews and so on.
    /// </summary>
    public class ApplicationsBL
    {
        //Workers Point-Of-View:::
        public void ApplyForPosition(int workerId, int positionId)
        {
            AddApplication(workerId, positionId);
        }

        private void AddApplication(int workerId, int positionId)
        {
            using (var db = new JobyJobsDB2())
            {
                AddApplication(workerId, positionId, db);
                db.SaveChanges();
            }
        }

        private void AddApplication(int workerId, int positionId, JobyJobsDB2 db)
        {
            var application = new application()
            {
                status_id = (int)Enums.ApplicationStatuses.New,
                position_id = positionId,
                worker_id = workerId,
            };
            db.applications.Add(application);
        }


        //Company Point-Of-View:::
        /// <summary>
        /// Returns all ongoing application for the current company.
        /// </summary>
        /// <returns></returns>
        public List<Application> GetOngoingApplications(int companyId)
        {
            using (var db = new JobyJobsDB2())
            {
                var items = db.applications
                    .Where(app => app.position.company_id == companyId &&
                        app.status_id == (int)Enums.ApplicationStatuses.New ||
                        app.status_id == (int)Enums.ApplicationStatuses.AfterPhoneInterview ||
                        app.status_id == (int)Enums.ApplicationStatuses.InvitedForInterview ||
                        app.status_id == (int)Enums.ApplicationStatuses.InProcessAfterInterview)
                    .AsEnumerable()
                    .Select(ServiceObjectsExtensions.ToSvc)
                    /*.OrderBy(item => item.Precedence)*/.ToList();
                return items.ToList();
            }

        }
        /// <summary>
        /// Returns all ongoing application for the current company, and the given position.
        /// </summary>
        /// <returns></returns>
        public List<Application> GetOngoingApplications(int companyId, int positionId)
        {
            using (var db = new JobyJobsDB2())
            {
                var items = db.applications
                    .Where(app => app.position_id == positionId &&
                            app.status_id == (int)Enums.ApplicationStatuses.New ||
                            app.status_id == (int)Enums.ApplicationStatuses.AfterPhoneInterview ||
                            app.status_id == (int)Enums.ApplicationStatuses.InvitedForInterview ||
                            app.status_id == (int)Enums.ApplicationStatuses.InProcessAfterInterview)
                    .AsEnumerable()
                    .Select(ServiceObjectsExtensions.ToSvc)
                    /*.OrderBy(item => item.Precedence)*/.ToList();
                return items.ToList();
            }

        }

        /// <summary>
        /// Sets an interview of the given information.
        /// </summary>
        /// <param name="interview"></param>
        public static void SetInterview(Interview item)
        {
            using (var db = new JobyJobsDB2())
            {
                db.interviews.Add(item.ToDB());
            }
        }

        public static void CancelInterview(int workerId, string cancelReason)
        {
            using (var db = new JobyJobsDB2())
            {
                interview interview;
                if ((interview = db.interviews.FirstOrDefault(item => item.worker_id == workerId)) == null)
                {
                    throw new JobyJobsException("Worker has no active interview");
                }
                interview.is_cancelled = true;
                interview.cancel_reason = cancelReason;
                db.Entry(interview).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
