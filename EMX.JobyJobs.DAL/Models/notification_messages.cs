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
    
    public partial class notification_messages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public notification_messages()
        {
            this.notification_messages_languages = new HashSet<notification_messages_languages>();
        }
    
        public int message_id { get; set; }
        public int notification_type_id { get; set; }
        public string header { get; set; }
        public string content { get; set; }
        public Nullable<System.DateTime> last_updated { get; set; }
        public Nullable<bool> active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<notification_messages_languages> notification_messages_languages { get; set; }
        public virtual notification_types notification_types { get; set; }
    }
}
