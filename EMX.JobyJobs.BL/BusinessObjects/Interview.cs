using System;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public class Interview
  {
    public int InterviewId { get; set; }
    public Guid InterviewUID { get; set; }
    public int EmployerId { get; set; }
    public int ApplicationId { get; set; }
    public int SeekerId { get; set; }
    /// <summary>
    /// The data of the invitation.
    /// </summary>
    public DateTime InviteDate { get; set; }
    /// <summary>
    /// The date of the actual interview.
    /// </summary>
    public DateTime DueDate { get; set; }
    /// <summary>
    /// Location inside the company.
    /// </summary>
    public string Location { get; set; }
    /// <summary>
    /// General notes.
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// Holds the invitation status.
    /// </summary>
    public Enums.InviteStatus InviteStatus { get; set; }
    public string InviteCancelRejectReason { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Active { get; set; }
    public Interview()
    {

    }

    public Interview(int interviewId, Guid interviewUID, int employerId, int applicationId, int seekerId, DateTime inviteDate, DateTime dueDate, string location, string notes, Enums.InviteStatus inviteStatus, string inviteCancelRejectReason, DateTime lastUpdated, bool active)
    {
      InterviewId = interviewId;
      InterviewUID = interviewUID;
      EmployerId = employerId;
      ApplicationId = applicationId;
      SeekerId = seekerId;
      InviteDate = inviteDate;
      DueDate = dueDate;
      Location = location;
      Notes = notes;
      InviteStatus = inviteStatus;
      InviteCancelRejectReason = inviteCancelRejectReason;
      LastUpdated = lastUpdated;
      Active = active;
    }
    public Interview CompleteCreate(interview item)
    {
      InviteStatus = (Enums.InviteStatus)item.invite_status_id;
      InviteCancelRejectReason = item.invite_cancelReject_reason;
      ApplicationId = item.application_id.Value;
      return this;
    }
  }
}
