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
    
    public partial class profession
    {
        public int profession_id { get; set; }
        public int field_id { get; set; }
        public string title { get; set; }
        public string tag_id { get; set; }
        public bool active { get; set; }
        public string professionscol { get; set; }
    }
}