using System;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public class LookupId
  {
    public enum LookupMethod
    {
      Unspecified = 0,
      ById = 1,
      ByUID = 2,
    }

    public LookupMethod Method { get; set; }
    public int? Id { get; set; }
    public Guid? UID { get; set; }

    public LookupId(int id)
    {
      Id = id;
      UID = null;
      Method = LookupMethod.ById;
    }

    public LookupId(Guid uid)
    {
      UID = uid;
      Id = null;
      Method = LookupMethod.ByUID;
    }

    public bool IsById()
    {
      return Method == LookupMethod.ById;
    }
  }
}