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
    
    public partial class company_persons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public company_persons()
        {
            this.company_person_settings = new HashSet<company_person_settings>();
        }
    
        public int company_person_id { get; set; }
        public string identity_user_id { get; set; }
        public int company_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public bool active { get; set; }
        public System.DateTime last_update { get; set; }
    
        public virtual company company { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<company_person_settings> company_person_settings { get; set; }
        public virtual user user { get; set; }
    }
}
