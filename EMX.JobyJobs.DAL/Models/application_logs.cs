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
    
    public partial class application_logs
    {
        public int application_id { get; set; }
        public int log_id { get; set; }
        public string text { get; set; }
    
        public virtual application application { get; set; }
    }
}