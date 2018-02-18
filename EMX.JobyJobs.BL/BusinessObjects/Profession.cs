using System;
using EMX.JobyJobs.DAL.Models;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public class Profession
  {
    public int ProfessionId { get; set; }
    public int FieldId { get; set; }
    public string Title { get; set; }
    public bool Active { get; set; }

    public Profession()
    {

    }

    public Profession(int professionId, int fieldId, string title, Guid tagId, bool isActive)
    {
      ProfessionId = professionId;
      FieldId = fieldId;
      Title = title;
      Active = isActive;
    }

    public Profession CompleteCreate(profession item)
    {
      return this;
    }
  }
}
