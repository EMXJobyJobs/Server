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
    
    public partial class seeker_resumes
    {
        public int id { get; set; }
        public int seeker_id { get; set; }
        public string cv_file { get; set; }
        public Nullable<System.DateTime> cv_upload_date { get; set; }
        public Nullable<System.DateTime> last_updated { get; set; }
        public bool active { get; set; }
    
        public virtual seeker seeker { get; set; }
    }
}