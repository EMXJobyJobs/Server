using EMX.JobyJobs.DAL.Models;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public class Field
  {
    public int FieldId { get; set; }
    public string Title { get; set; }
    public bool Active { get; set; }

    public Field()
    {

    }

    public Field(int fieldId, string title, bool active)
    {
      FieldId = fieldId;
      Title = title;
      Active = active;
    }
    public Field CompleteCreate(field item)
    {
      return this;
    }
  }
}
