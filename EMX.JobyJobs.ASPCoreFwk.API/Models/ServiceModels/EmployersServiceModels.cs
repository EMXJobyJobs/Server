namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{
  public class UpdateEmployerServiceRequest
  {
    public int EmployerId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string ContantPersonName { get; set; }
    public string ContactPersonPhoneNumber { get; set; }
    public string EmployerHP { get; set; }
    public string About { get; set; }
    public string WebSiteUrl { get; set; }
    /// <summary>
    /// The employer logo image file
    /// </summary>
    public string Logo { get; set; }
  }

  public class UpdateEmployerPersonServiceRequest
  {
    public int EmployerPersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string IdNumber { get; set; }
    public string JobFunction { get; set; }
  }

}
