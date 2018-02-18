using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMX.JobyJobs.Admin.ASPCoreFwk.Models
{
  public class CreateUpdatePositionViewModel    //also for update
  {
    public int PostionId { get; set; }    //0 for create
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

    public CreateUpdatePositionViewModel()
    {

    }

    public CreateUpdatePositionViewModel(int postionId, int employerId, string title, int fieldId, int professionId, string description, string jobRequirements, DateTime createDate, DateTime publishDate, int salaryMin, int salaryMax, string location, int positionType, bool exposeImmediately)
    {
      PostionId = postionId;
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
}
