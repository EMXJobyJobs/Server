﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMX.JobyJobs.Admin.MVC.Startup))]
namespace EMX.JobyJobs.Admin.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}