using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EMX.JobyJobs.ASPCoreFwk.API
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //initialize log4net.
      log4net.Config.XmlConfigurator.Configure();

      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(builder =>
            {
              builder.AddJsonFile("appsettings.json");
            })
            .UseStartup<Startup>()
            .Build();
  }
}
