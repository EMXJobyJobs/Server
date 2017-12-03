using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EMX.JobyJobs.MVC.Startup))]
namespace EMX.JobyJobs.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
