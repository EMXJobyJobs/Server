using System;

namespace EMX.JobyJobs.Shared.Definitions
{
  public class Consts
  {
    public static readonly Guid SeekerRoleGUID = new Guid("162B5DCC-6359-4DF5-944D-0575A372685A");
    public static readonly Guid EmployerRoleGUID = new Guid("B99F9807-F2CB-466A-863F-9F2C9F0FEA9D");
    public static readonly Guid AdminRoleGUID = new Guid("20F355E9-B56E-4E79-9C16-269239D559D7");

    public const int MainLanguageId = 1;
    public const string MainLanguageCode = "he";

    public const string JobyJobsSenderEmail = "support@jobyjobs.com";
    public const string JobyJobsCallbackHost = "www.jobyjobs.com";
  }
}
