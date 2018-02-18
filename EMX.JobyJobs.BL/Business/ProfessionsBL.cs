using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.BL.Managers;
using EMX.JobyJobs.DAL.Models;

namespace EMX.JobyJobs.BL.Business
{

  /// <summary>
  /// Handles all business-logic activities around professions, sub-professions, fields and so on.
  /// </summary>
  public class ProfessionsBL : IProfessionsBL
  {
    public ProfessionsHierarchy GetProfessionsHierarchy()
    {
      var hierarchy = new ProfessionsHierarchy();
      hierarchy.Fields = new Dictionary<int, ProfessionHierarchyField>();
      using (var db = new JobyJobsDB2())
      {
        var allFields = db.fields.ToList();
        foreach (var item in allFields)
        {
          hierarchy.Fields.Add(item.filed_id, new ProfessionHierarchyField());
          var allProfessions = db.fields.Single(item0 => item0.filed_id == item.filed_id).professions;
          foreach (var item1 in allProfessions)
          {
            hierarchy.Fields[item.filed_id].Professions.Add(item1.profession_id, item1.ToBusiness());
          }
        }
      }
      return hierarchy;
    }
  }

  public interface IProfessionsBL : IBusiness
  {
    ProfessionsHierarchy GetProfessionsHierarchy();
  }

}
