using System.Collections.Generic;

namespace EMX.JobyJobs.BL.BusinessObjects
{

  public class ProfessionsHierarchy
  {
    public Dictionary<int, ProfessionHierarchyField> Fields { get; set; }   //Dictionary. K: fieldId, V: fieldObject.

    public ProfessionsHierarchy()
    {
      Fields = new Dictionary<int, ProfessionHierarchyField>();
    }
  }

  public class ProfessionHierarchyField : Field
  {
    public Dictionary<int, Profession> Professions { get; set; }    //Dictionary. K: professionId, V: professionObject.

    public ProfessionHierarchyField()
    {
      Professions = new Dictionary<int, Profession>();
    }
  }
}