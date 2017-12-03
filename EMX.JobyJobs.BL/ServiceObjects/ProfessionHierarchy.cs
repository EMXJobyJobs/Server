using System.Collections.Generic;

namespace EMX.JobyJobs.BL.ServiceObjects
{
    public class ProfessionHierarchy
    {
        public Dictionary<int, ProfessionHierarchyField> Data { get; set; }   //Dictionary. K: fieldId, V: fieldObject.

        public ProfessionHierarchy()
        {
            Data = new Dictionary<int, ProfessionHierarchyField>();
        }
    }

    public class ProfessionHierarchyField
    {
        public Dictionary<int, ProfessionHierarchyFieldProfession> Professions { get; set; }    //Dictionary. K: professionId, V: professionObject.

        public ProfessionHierarchyField()
        {
            Professions = new Dictionary<int, ProfessionHierarchyFieldProfession>();
        }
    }

    public class ProfessionHierarchyFieldProfession
    {
        public Dictionary<int, ProfessionHierarchyFieldProfession> SubProfessions { get; set; }     //Dictionary. K: professionId, V: professionData.
        public bool IsApplicable { get; set; }
        public ProfessionHierarchyFieldProfession()
        {
            SubProfessions = new Dictionary<int, ProfessionHierarchyFieldProfession>();
            IsApplicable = true;
        }
    }

}