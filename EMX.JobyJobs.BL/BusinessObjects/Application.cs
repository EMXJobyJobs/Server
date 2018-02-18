using System;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  /// <summary>
  /// Represents a job application.
  /// </summary>
  public class Application
  {
    public int Id { get; set; }
    public int SeekerId { get; set; }
    public int PositionId { get; set; }
    public Enums.ApplicationStatuses Status { get; set; }
    public DateTime ApplicationStartDate { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Active { get; set; }

    public Application()
    {

    }

    public Application(int id, int seekerId, int positionId, Enums.ApplicationStatuses status, DateTime applicationStartDate, DateTime lastUpdated, bool active)
    {
      Id = id;
      SeekerId = seekerId;
      PositionId = positionId;
      Status = status;
      ApplicationStartDate = applicationStartDate;
      LastUpdated = lastUpdated;
      Active = active;
    }
    public Application(application item)
    {
      Id = item.id;
      SeekerId = item.seeker_id;
      PositionId = item.position_id;
      Status = (Enums.ApplicationStatuses)item.status_id;
      ApplicationStartDate = item.application_start_date;
      LastUpdated = item.last_updated.GetValueOrDefault();
      Active = item.active.GetValueOrDefault();
    }

    public Application CompleteCreate(application item)
    {
      this.Status = (Enums.ApplicationStatuses)item.status_id;
      return this;
    }
  }
}
