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
    
    public partial class conversation_messages
    {
        public int message_id { get; set; }
        public string message_uid { get; set; }
        public int message_type { get; set; }
        public int seeker_id { get; set; }
        public int employer_id { get; set; }
        public int employer_person_id { get; set; }
        public string header { get; set; }
        public string content { get; set; }
        public System.DateTime date { get; set; }
        public bool is_read { get; set; }
    
        public virtual employer employer { get; set; }
        public virtual employer_persons employer_persons { get; set; }
        public virtual seeker seeker { get; set; }
    }
}
