using System;
using System.ComponentModel.DataAnnotations;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{
  public class ApplyForPositionServiceRequest
  {
    [Required]
    public int SeekerId { get; set; }

    //public LookupId PositionId { get; set; }
    public int? PositionId { get; set; } //Alt: PositionId

    public Guid? PositionUID { get; set; } //Alt: PositionUID
  }

  public class DropApplicationServiceRequest
  {
    [Required]
    public int SeekerId { get; set; }

    [Required]
    public int ApplicationId { get; set; }
  }


  public class DropAllApplicationsServiceRequest
  {
    [Required]
    public int SeekerId { get; set; }
  }

  public class GetPositionApplicationsServiceRequest
  {
    public int EmployerId { get; set; }
    public int PositionId { get; set; }
  }

  public class GetAllApplicationsServiceRequest
  {
    public int EmployerId { get; set; }
  }

  public class UpdateApplicationStatusServiceRequest
  {
    public int ApplicationId { get; set; }
    public Enums.ApplicationStatuses NewStatus { get; set; }
  }



  public class SetInterviewServiceRequest
  {
    public int EmployerId { get; set; }
    public int SeekerId { get; set; }
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
  }

  public class CancelInterviewServiceRequest
  {
    public int SeekerId { get; set; }
    public int EmployerId { get; set; }
    public string CancelReason { get; set; }
  }
  public class UpdateInterviewServiceRequest
  {
    public int InterviewId { get; set; }
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
  }
  public class RespondInterviewServiceRequest
  {
    public int InterviewId { get; set; }
    /// <summary>
    /// Note: Null if not relevant
    /// </summary>
    public string RejectReason { get; set; }
  }
  public class UpdateApplicationServiceRequest
  {
    public int ApplicationId { get; set; }
    public Enums.ApplicationStatuses Status { get; set; }
  }
  public class GetMatchedCandidatesServiceRequest
  {
    [Required]
    public int EmployerPersonId { get; set; }
    [Required]
    public int PositionId { get; set; }
  }

  public class SetInterviewNotesServiceRequest
  {
    public int InterviewId { get; set; }
    public string Notes { get; set; }
  }

}