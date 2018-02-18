using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMX.JobyJobs.ASPCoreFwk.API.Controllers;
using EMX.JobyJobs.ASPCoreFwk.API.Helpers;
using EMX.JobyJobs.ASPCoreFwk.Shared.Helpers;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.BL.Helpers;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.BL.Managers;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.ProxyServices.Managers;
using EMX.JobyJobs.Shared.Helpers;
using log4net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using LinkGenerator = EMX.JobyJobs.ASPCoreFwk.API.Helpers.LinkGenerator;

namespace EMX.JobyJobs.ASPCoreFwk.API
{
  public class Startup
  {
    private static readonly ILog m_logger = LogManager.GetLogger(typeof(HomeController));

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    // Note: Adds Services
    public void ConfigureServices(IServiceCollection services)
    {
      m_logger.Error("Test: Startup.ConfigureServices() : Start");
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(jwtBearerOptions =>
        {
          jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateActor = true,
            ValidateAudience = /*true*/ false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration.Get<AppSettings>().JWT.JwtIssuer,
            //ValidAudience = Configuration.Get<AppSettings>()."JWTAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Get<AppSettings>().JWT.JwtSigningKey))
          };
        });

      services.AddMvc();
      services.AddSignalR();

      //user code here:::
      services.AddAutoMapper(typeof(AutoMapperProfile1), typeof(AutoMapperProfile2), typeof(AutoMapperProfile3), typeof(AutoMapperProfile4));
      ConfigureService_DI(services);
      m_logger.Error("Test: Startup.ConfigureServices() : End");
    }

    public void ConfigureService_DI(IServiceCollection services)
    {
      //singleton:
      //bl
      services.AddSingleton<IUsersBL, UsersBL>();
      services.AddSingleton<ISeekersBL, SeekersBL>();
      services.AddSingleton<IEmployersBL, EmployersBL>();
      services.AddSingleton<IAdminBL, AdminBL>();
      services.AddSingleton<IPositionsBL, PositionsBL>();
      services.AddSingleton<IProfessionsBL, ProfessionsBL>();
      services.AddSingleton<IApplicationsBL, ApplicationsBL>();
      services.AddSingleton<IReactionsBL, ReactionsBL>();
      services.AddSingleton<IConversationsBL, ConversationsBL>();
      services.AddSingleton<EMX.JobyJobs.BL.Business.ILinkGenerator, LinkGenerator>();
      //managers
      services.AddSingleton<ICacheManager, CacheManager>();
      services.AddSingleton<IBLCacheSource, BLCacheSource>();
      services.AddSingleton<IGeneralManager, GeneralManager>();
      services.AddSingleton<ICryptographyHelper, AspCoreCryptographyHelper>();
      services.AddSingleton<IUserStateManager, UserStateManager>();
      //proxy services
      services.AddSingleton<IChatManager, ChatManager>();
      services.AddSingleton<IEmailSender, EmailSender>();
      //services.AddSingleton<IUserStateManager, SMSSender>();
      //services.AddSingleton<IUserStateManager, PushService>();
      //services.AddSingleton<IUserStateManager, CitiesManager>();
      //services.AddSingleton<IUserStateManager, NotificationEngineManager>();


      //scoped:

      //services.AddDbContext<JobyJobsDB2>();
      services.AddScoped<JobyJobsDB2>();
      //cruds
      services.AddScoped<CRUD<user>>();
      services.AddScoped<CRUD<seeker>>();
      services.AddScoped<CRUD<employer>>();
      services.AddScoped<CRUD<employer_persons>>();
      services.AddScoped<CRUD<admin_persons>>();
      services.AddScoped<CRUD<position>>();
      services.AddScoped<CRUD<application>>();
      services.AddScoped<CRUD<interview>>();
      services.AddScoped<CRUD<reaction>>();
      services.AddScoped<CRUD<profession>>();
      services.AddScoped<CRUD<field>>();
      services.AddScoped<CRUD<conversation_messages>>();



      //transient:


    }


    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // Note: Configures Services
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider sp)
    {
      m_logger.Error("Test: Startup.Configure() : Start");
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
        {
          HotModuleReplacement = true
        });
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      if (env.IsDevelopment())
      {
        //test: read raw request.
        //app.Use(next => context => { context.Request.EnableRewind(); return next(context); });
      }
      app.UseAuthentication();
      app.UseCors(builder =>
        builder.WithOrigins("http://www.jobyjobs.com", "http://joby.ori-pc.com")
          .AllowAnyOrigin()     //Todo. find a better solution.
          .AllowAnyHeader()
          .AllowAnyMethod());   //Note: allow CORS;
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");

        routes.MapSpaFallbackRoute(
                  name: "spa-fallback",
                  defaults: new { controller = "Home", action = "Index" });
      });
      app.UseSignalR(routes =>
      {
        routes.MapHub<ChatHub>("chat");
        routes.MapHub<NotificationsHub>("notifications");
      });

      //user code here:
      //Initializes cache.
      SiteExtensions.Initialize(Configuration);
      sp.GetService<ICacheManager>().Initialize(sp.GetService<IBLCacheSource>());
      m_logger.Error("Test: Startup.Configure() : End");
    }
  }
}
