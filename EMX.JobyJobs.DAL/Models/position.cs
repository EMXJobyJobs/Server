//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMX.JobyJobs.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class position
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public position()
        {
            this.applications = new HashSet<application>();
        }
    
        public int position_id { get; set; }
        public string position_uid { get; set; }
        public int company_id { get; set; }
        public string position_type { get; set; }
        public string title { get; set; }
        public int profession_id { get; set; }
        public int subprofession_id { get; set; }
        public int salary_min { get; set; }
        public int salary_max { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public int status_id { get; set; }
        public Nullable<int> internal_precedence { get; set; }
        public bool draft { get; set; }
        public bool frozen { get; set; }
        public bool active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<application> applications { get; set; }
    }
}
