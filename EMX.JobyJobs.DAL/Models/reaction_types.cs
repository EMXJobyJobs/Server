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
    
    public partial class reaction_types
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public reaction_types()
        {
            this.reactions = new HashSet<reaction>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<reaction> reactions { get; set; }
    }
}
