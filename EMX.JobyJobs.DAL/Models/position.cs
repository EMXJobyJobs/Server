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
            this.conversation_messages = new HashSet<conversation_messages>();
            this.position_settings = new HashSet<position_settings>();
            this.reactions = new HashSet<reaction>();
            this.positions_languages = new HashSet<positions_languages>();
            this.employer_persons = new HashSet<employer_persons>();
        }
    
        public int position_id { get; set; }
        public string position_uid { get; set; }
        public int employer_id { get; set; }
        public Nullable<int> position_type { get; set; }
        public string title { get; set; }
        public int profession_id { get; set; }
        public Nullable<int> subprofession_id { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<System.DateTime> publish_date { get; set; }
        public int salary_min { get; set; }
        public int salary_max { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public int status_id { get; set; }
        public Nullable<int> rank { get; set; }
        public Nullable<System.DateTime> last_updated { get; set; }
        public Nullable<bool> active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<application> applications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<conversation_messages> conversation_messages { get; set; }
        public virtual employer employer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<position_settings> position_settings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reaction> reactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<positions_languages> positions_languages { get; set; }
        public virtual profession profession { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<employer_persons> employer_persons { get; set; }
    }
}
