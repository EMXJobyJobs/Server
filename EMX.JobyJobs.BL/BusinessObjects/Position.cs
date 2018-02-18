using System;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public class Position
  {
    public int PositionId { get; set; }
    public Guid PositionUID { get; set; }
    public int EmployerId { get; set; }
    public Enums.PositionTypes PositionType { get; set; }
    public string Title { get; set; }
    public int ProfessionId { get; set; }
    public int SubprofessionId { get; set; }    //not used.
    public DateTime CreateDate { get; set; }
    public DateTime? PublishDate { get; set; }
    public int SalaryMin { get; set; }
    public int SalaryMax { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public Enums.PositionStatuses Status { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Active { get; set; }

    public Position()
    {

    }

    public Position(int positionId, Guid positionUID, int employerId, Enums.PositionTypes positionType, string title, int professionId, int subprofessionId, DateTime createDate, DateTime? publishDate, int salaryMin, int salaryMax, string location, string description, Enums.PositionStatuses status, DateTime lastUpdated, bool active)
    {
      PositionId = positionId;
      PositionUID = positionUID;
      EmployerId = employerId;
      PositionType = positionType;
      Title = title;
      ProfessionId = professionId;
      SubprofessionId = subprofessionId;
      CreateDate = createDate;
      PublishDate = publishDate;
      SalaryMin = salaryMin;
      SalaryMax = salaryMax;
      Location = location;
      Description = description;
      Status = status;
      LastUpdated = lastUpdated;
      Active = active;
    }
    public Position CompleteCreate(position item)
    {
      this.PositionUID = new Guid(item.position_uid);
      this.PositionType = (Enums.PositionTypes)item.position_type.GetValueOrDefault(0);
      this.SubprofessionId = item.subprofession_id.GetValueOrDefault(0);
      this.Status = (Enums.PositionStatuses)item.status_id;
      this.Active = item.active.GetValueOrDefault(true);

      return this;
    }
  }
}
