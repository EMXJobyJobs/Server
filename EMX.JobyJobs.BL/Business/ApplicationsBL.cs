using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Exceptions;
using EMX.JobyJobs.Shared.Helpers;

namespace EMX.JobyJobs.BL.Business
{
  public interface IApplicationsBL : IBusiness
  {
    CRUD<application> ApplicationsCRUD { get; set; }
    CRUD<interview> InterviewsCRUD { get; set; }

    void ApplyForPosition(int seekerId, int? positionId, Guid? positionUID);
    void DropApplication(int seekerId, int applicationId);
    void DropAllApplications(int seekerId);
    Interview GetInterview(int interviewId);
    Interview GetInterviewByUID(Guid interviewId);
    void ApproveInterview(int interviewId);
    void RejectInterview(int interviewId, string rejectReason);
    List<Application> GetPositionApplications(int employerId, int positionId);
    List<Interview> GetPositionInterviews(int positionId);
    List<Application> GetAllApplications(int employerId);
    void SetInterview(Interview item);
    void CancelInterview(int seekerId, int employerId, string cancelReason);
    void UpdateInterview(int interviewId, DateTime dueDate, string location, string notes);
    void UpdateApplicationStatus(int modelApplicationId, Enums.ApplicationStatuses modelNewStatus);
    Application GetApplication(int applicationId);
    string GetCandidateCVFile(int applicationId);
    List<Application> GetMyApplications(int seekerId);
    List<Seeker> GetHiredCandidates(int positionId);
    void SetInterviewNotes(int dataInterviewId, string dataNotes);
  }
  /// <summary>
  /// Handles all business-logic activities around applications(and its candidates), interviews and so on.
  /// </summary>
  public class ApplicationsBL : IApplicationsBL
  {
    private ISeekersBL _seekersBL;

    /// <summary>
    /// applications crud repository.
    /// </summary>
    public CRUD<application> ApplicationsCRUD { get; set; }
    /// <summary>
    /// interviews crud repository.
    /// </summary>
    public CRUD<interview> InterviewsCRUD { get; set; }

    public ApplicationsBL(ISeekersBL seekersBL)
    {
      ApplicationsCRUD = new CRUD<application>();
      InterviewsCRUD = new CRUD<interview>();
      _seekersBL = seekersBL;
    }


    //Seekers Point-Of-View:::
    public void ApplyForPosition(int seekerId, int? positionId, Guid? positionUID)
    {
      AddApplication(seekerId, positionId, positionUID);
    }

    /// <summary>
    /// First looks by positionId then by positionUID.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <param name="positionId"></param>
    /// <param name="positionUID"></param>
    private void AddApplication(int seekerId, int? positionId, Guid? positionUID)
    {
      using (var db = new JobyJobsDB2())
      {
        int lPositionId = positionId ?? db.positions.Single(item => item.position_uid == positionUID.ToString()).position_id;
        if (db.applications.Any(app => app.active == true && app.seeker_id == seekerId && app.position_id == lPositionId))  //Todo. move to db trigger.
        {   //seeker already applied for the job, throw.
          throw new JobyJobsException("Seeker already applied for the given position");
        }

        var application = new application()
        {
          position_id = lPositionId,
          seeker_id = seekerId,
          status_id = (int)Enums.ApplicationStatuses.New,
          application_start_date = DateTime.Now,
          active = true
        };
        db.applications.Add(application);
        db.SaveChanges();
      }
    }

    public List<Application> GetMyApplications(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        return db.applications.Where(item => item.seeker_id == seekerId).ToSelectedList(BusinessObjectsExtensions.ToBusiness);
      }
    }

    public void DropApplication(int seekerId, int applicationId)
    {
      using (var db = new JobyJobsDB2())
      {
        var app = db.applications.Single(item => item.seeker_id == seekerId && item.id == applicationId);
        app.status_id = (int)Enums.ApplicationStatuses.NotInterested;
        db.Entry<application>(app).State = EntityState.Modified;
        db.SaveChanges();
      }
    }
    public void DropAllApplications(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var ongoingStatuses = new Enums.ApplicationStatuses[5]
        {
          Enums.ApplicationStatuses.New, Enums.ApplicationStatuses.AfterPhoneInterview,
          Enums.ApplicationStatuses.InvitedForInterview, Enums.ApplicationStatuses.InProcessAfterInterview,
          Enums.ApplicationStatuses.Hired
        }.ToSelectedList(item => (int)item);
        var app = db.applications.Where(item => item.seeker_id == seekerId &&
                                                ongoingStatuses.Contains(item.status_id));
        db.SaveChanges();
      }
    }
    /// <summary>
    /// Seeker- returns the interview of the given unique id.
    /// Note: retrieves the interviews and marks it as opened.
    /// </summary>
    /// <param name="interviewId"></param>
    /// <returns></returns>
    public Interview GetInterviewByUID(Guid interviewUID)
    {
      using (var db = new JobyJobsDB2())
      {
        var obj = db.interviews.Single(item => item.active == true && item.interview_uid == interviewUID.ToString());
        obj.invite_status_id = (int)Enums.InviteStatus.Opened;
        db.Entry(obj).State = EntityState.Modified;

        db.SaveChanges();

        return obj.ToBusiness();
      }
    }

    public void ApproveInterview(int interviewId)
    {
      using (var db = new JobyJobsDB2())
      {
        var obj = db.interviews.Single(item => item.interview_id == interviewId);
        obj.invite_status_id = (int)Enums.InviteStatus.Accepted;
        db.Entry(obj).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    public void RejectInterview(int interviewId, string rejectReason)
    {
      using (var db = new JobyJobsDB2())
      {
        interview interview = db.interviews.Single(item => item.interview_id == interviewId);
        interview.invite_status_id = (int)Enums.InviteStatus.Rejected;
        interview.invite_cancelReject_reason = rejectReason;
        db.Entry(interview).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    //simple CRUD
    public Interview GetInterview(int interviewId)
    {
      return InterviewsCRUD.Get(interviewId).ToBusiness();
    }



    //Employer Point-Of-View:::
    /// <summary>
    /// Returns all applications for the given position.
    /// </summary>
    /// <returns></returns>
    public List<Application> GetPositionApplications(int employerId, int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        var items = db.applications
          .Where(app => app.position.employer_id == employerId &&
                        app.position.position_id == positionId &&
                        app.status_id == (int)Enums.ApplicationStatuses.New ||
                        app.status_id == (int)Enums.ApplicationStatuses.AfterPhoneInterview ||
                        app.status_id == (int)Enums.ApplicationStatuses.InvitedForInterview ||
                        app.status_id == (int)Enums.ApplicationStatuses.InProcessAfterInterview)
          .Select(BusinessObjectsExtensions.ToBusiness)
          /*.OrderBy(item => item.Precedence)*/.ToList();
        return items.ToList();
      }
    }



    public Application GetApplication(int applicationId)
    {
      return ApplicationsCRUD.Get(applicationId).ToBusiness();
    }

    /// <summary>
    /// Returns the relative file path of the CV of the seeker that had applied to the job in the current application.
    /// Also marks the current application as watched.
    /// Note: relative to the user-files folder.
    /// </summary>
    /// <param name="applicationId"></param>
    public string GetCandidateCVFile(int applicationId)
    {
      using (var db = new JobyJobsDB2())
      {
        //gets the cv file path.
        var dbApplication = ApplicationsCRUD.Get(applicationId);
        if (dbApplication == null)
        {
          throw new JobyJobsException("no application with the given id");
        }
        string cvFilePath = _seekersBL.GetSeekerInternal(dbApplication.seeker_id, db).CV_File;

        //mark the application as watched by the current employer.
        this.MarkAsWatched(applicationId, db);

        //save db changes.
        db.SaveChanges();

        //returns it.
        return cvFilePath;
      }
    }

    /// <summary>
    /// Note: Continous function. does not commit.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <param name="db"></param>
    public void MarkAsWatched(int applicationId, JobyJobsDB2 db)
    {
      var obj = ApplicationsCRUD.Get(applicationId, db);
      obj.watched = true;

      db.SetAsModified(obj);
    }


    public List<Seeker> GetHiredCandidates(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        return db.applications.Where(item => item.active != false && item.status_id == (int)Enums.ApplicationStatuses.Hired)
          .Select(item => item.seeker)
          .Include(item => item.user).Include(item => item.seeker_resumes).Include(item => item.seeker_job_interests)
          .ToSelectedList(BusinessObjectsExtensions.ToBusiness);
      }
    }

    /// <summary>
    /// Returns all interviews for the given position.
    /// </summary>
    /// <param name="applicationId"></param>
    /// <returns></returns>
    public List<Interview> GetPositionInterviews(int positionId)
    {
      using (var db = new JobyJobsDB2())
      {
        return db.interviews.Where(item => item.application.position_id == positionId)
               .ToSelectedList(BusinessObjectsExtensions.ToBusiness);
      }
    }

    public void SetInterviewNotes(int interviewId, string notes)
    {
      using (var db = new JobyJobsDB2())
      {
        var interview = InterviewsCRUD.Get(interviewId, db);
        interview.notes = notes;

        db.SetAsModifiedAndSave(interview);
      }
    }



    /// <summary>
    /// Returns all applications for any position of the given employer.
    /// </summary>
    /// <returns></returns>
    public List<Application> GetAllApplications(int employerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var items = db.applications
          .Where(app => app.position.employer_id == employerId &&
                        app.status_id == (int)Enums.ApplicationStatuses.New ||
                        app.status_id == (int)Enums.ApplicationStatuses.AfterPhoneInterview ||
                        app.status_id == (int)Enums.ApplicationStatuses.InvitedForInterview ||
                        app.status_id == (int)Enums.ApplicationStatuses.InProcessAfterInterview)
          .Select(BusinessObjectsExtensions.ToBusiness)
          /*.OrderBy(item => item.Precedence)*/.ToList();
        return items.ToList();
      }
    }
    /// <summary>
    /// Sets an interview of the given information.
    /// </summary>
    /// <param name="interview"></param>
    public void SetInterview(Interview item)
    {
      using (var db = new JobyJobsDB2())
      {
        //fixes db object.
        var dateTimeNow = DateTime.Now;
        item.InterviewUID = Guid.NewGuid();
        item.InviteDate = dateTimeNow;
        item.InviteStatus = Enums.InviteStatus.Sent;   //Note: marks the invitation as sent.
        item.LastUpdated = dateTimeNow;
        item.Active = true;
        db.interviews.Add(item.ToDB());

        db.SaveChanges();
      }
    }

    public void CancelInterview(int seekerId, int employerId, string cancelReason)
    {
      using (var db = new JobyJobsDB2())
      {
        interview interview;
        if (!db.interviews.GetAny(item => item.active == true && item.seeker_id == seekerId && item.employer_id == employerId, out interview))
        {
          throw new JobyJobsException("Seeker has no active interview with the specified employer");
        }

        interview.invite_status_id = (int)Enums.InviteStatus.Cancelled;
        interview.invite_cancelReject_reason = cancelReason;
        db.Entry(interview).State = EntityState.Modified;

        db.SaveChanges();
      }
    }
    /// <summary>
    /// Updates the interview of the given information.
    /// </summary>
    /// <param name="interview"></param>
    public void UpdateInterview(int interviewId, DateTime dueDate, string location, string notes)
    {
      using (var db = new JobyJobsDB2())
      {
        interview interview;
        if (!db.interviews.GetAny(item => item.interview_id == interviewId, out interview))
        {
          throw new JobyJobsException("The Requested interview was not found");
        }
        interview.due_date = dueDate;
        interview.location = location;
        interview.notes = notes;
        db.Entry(interview).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    public void UpdateApplicationStatus(int applicationId, Enums.ApplicationStatuses newStatus)
    {
      using (var db = new JobyJobsDB2())
      {
        var application = db.applications.Single(item => item.id == applicationId);

        application.status_id = (int)newStatus;
        db.SetAsModifiedAndSave(application);
      }
    }


    ///// <summary>
    ///// Returns all ongoing application for the current company.
    ///// </summary>
    ///// <returns></returns>
    //public   List<Application> GetOngoingApplications(int employerId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.applications
    //        .Where(app => app.position.employer_id == employerId &&
    //            app.status_id == (int)Enums.ApplicationStatuses.New ||
    //            app.status_id == (int)Enums.ApplicationStatuses.AfterPhoneInterview ||
    //            app.status_id == (int)Enums.ApplicationStatuses.InvitedForInterview ||
    //            app.status_id == (int)Enums.ApplicationStatuses.InProcessAfterInterview)
    //        .AsEnumerable()
    //        .Select(ServiceObjectsExtensions.ToBusiness)
    //        /*.OrderBy(item => item.Precedence)*/.ToList();
    //    return items.ToList();
    //  }

    //}
    ///// <summary>
    ///// Returns all ongoing application for the current company, and the given position.
    ///// </summary>
    ///// <returns></returns>
    //public   List<Application> GetOngoingApplications(int employerId, int positionId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    var items = db.applications
    //        .Where(app => app.position_id == positionId &&
    //                app.status_id == (int)Enums.ApplicationStatuses.New ||
    //                app.status_id == (int)Enums.ApplicationStatuses.AfterPhoneInterview ||
    //                app.status_id == (int)Enums.ApplicationStatuses.InvitedForInterview ||
    //                app.status_id == (int)Enums.ApplicationStatuses.InProcessAfterInterview)
    //        .AsEnumerable()
    //        .Select(ServiceObjectsExtensions.ToBusiness)
    //        /*.OrderBy(item => item.Precedence)*/.ToList();
    //    return items.ToList();
    //  }

    //}

    ///// <summary>
    ///// Sets an interview of the given information.
    ///// </summary>
    ///// <param name="interview"></param>
    //public   void SetInterview(Interview item)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    db.interviews.Add(item.ToDB());
    //  }
    //}

    //public   void CancelInterview(int workerId, string cancelReason)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    interview interview;
    //    if ((interview = db.interviews.FirstOrDefault(item => item.seeker_id == workerId)) == null)
    //    {
    //      throw new JobyJobsException("Worker has no active interview");
    //    }
    //    interview.is_cancelled = true;
    //    interview.cancel_reason = cancelReason;
    //    db.Entry(interview).State = EntityState.Modified;
    //    db.SaveChanges();
    //  }
    //}

    //public   List<Application> GetAllApplications(int employerId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    return db.applications
    //      .Where(item => item.position.employer_id == employerId).AsEnumerable()
    //      .Select(ServiceObjectsExtensions.ToBusiness).ToList();
    //  }
    //}

    //public   List<Application> GetAllApplications(int employerId, int positionId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    return db.applications
    //      .Where(item => item.position.employer_id == employerId &&
    //          item.position.position_id == positionId).AsEnumerable()
    //      .Select(ServiceObjectsExtensions.ToBusiness).ToList();
    //  }
    //}

    //public   object GetLikedApplications(int employerId, int positionId)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    return db.applications
    //      .Where(item => item.position.employer_id == employerId &&
    //                     item.position.position_id == positionId).AsEnumerable()
    //      .Select(ServiceObjectsExtensions.ToBusiness).ToList();
    //  }
    //}

  }


}
