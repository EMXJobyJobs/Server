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
    
    public partial class position_settings
    {
        public int position_id { get; set; }
        public string setting_key { get; set; }
        public string settings_value { get; set; }
        public System.DateTime last_updated { get; set; }
    
        public virtual position position { get; set; }
    }
}
