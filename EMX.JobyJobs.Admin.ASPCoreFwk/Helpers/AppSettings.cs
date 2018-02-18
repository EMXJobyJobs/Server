namespace EMX.JobyJobs.Admin.ASPCoreFwk.Helpers
{
  public class AppSettings
  {
    public JWT JWT { get; set; }
    public Logging Logging { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public General General { get; set; }
  }

  public class JWT
  {
    public string JwtSigningKey { get; set; }
    public string JwtIssuer { get; set; }
    public int JwtExpireDays { get; set; }
  }

  public class LogLevel
  {
    public string Default { get; set; }
  }

  public class Logging
  {
    public LogLevel LogLevel { get; set; }
  }

  public class ConnectionStrings
  {
    public string DefaultConnection { get; set; }
    public string JobyJobsDB { get; set; }
    public string JobyJobsDB2 { get; set; }
  }

  public class General
  {
    public SiteEnums.ClientUIMode ClientUIMode { get; set; }
    public string UserFilesUploadsFolder { get; set; }
    public string EmployerLogosUploadFolder { get; set; }
    public string EmployerPersonAvatarsUploadFolder { get; set; }
    public string SeekerAvatarsUploadFolder { get; set; }
    public string SeekerCVsUploadFolder { get; set; }
  }


}
