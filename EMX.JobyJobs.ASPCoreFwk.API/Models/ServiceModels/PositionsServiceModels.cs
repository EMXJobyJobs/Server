using System;
using System.ComponentModel.DataAnnotations;
using EMX.JobyJobs.Shared.Definitions;
using Remotion.Linq.Parsing.Structure.ExpressionTreeProcessors;

namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{

  //Seeker Point-of-View:::

  //public class SearchPositionViewModel
  //{

  //}

  //Employer Point-of-View:::
  public class PostPositionServiceRequest
  {
    [Required]
    public int EmployerId { get; set; }
    public string Title { get; set; }
    public int FieldId { get; set; }
    public int ProfessionId { get; set; }   //תפקיד
    public string Description { get; set; }
    public string JobRequirements { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime PublishDate { get; set; }
    public int SalaryMin { get; set; }
    public int SalaryMax { get; set; }
    public string Location { get; set; }
    public int PositionType { get; set; }
    public bool ExposeImmediately { get; set; }    //should set to 'OnAir'.

    public PostPositionServiceRequest()
    {

    }

    public PostPositionServiceRequest(int employerId, string title, int fieldId, int professionId, string description, string jobRequirements, DateTime createDate, DateTime publishDate, int salaryMin, int salaryMax, string location, int positionType, bool exposeImmediately)
    {
      EmployerId = employerId;
      Title = title;
      FieldId = fieldId;
      ProfessionId = professionId;
      Description = description;
      JobRequirements = jobRequirements;
      CreateDate = createDate;
      PublishDate = publishDate;
      SalaryMin = salaryMin;
      SalaryMax = salaryMax;
      Location = location;
      PositionType = positionType;
      ExposeImmediately = exposeImmediately;
    }
  }


  public class GetPositionsByStatusServiceRequest
  {
    //public int EmployerId { get; set; }    //Note: get from identity.
    public Enums.PositionStatuses Status { get; set; }   //active, frozen,draft,deleted
  }

  public class SetPositionStatusServiceRequest
  {
    public int PositionId { get; set; }
    public Enums.PositionStatuses NewStatus { get; set; }   //active, frozen,draft,deleted
  }
}
