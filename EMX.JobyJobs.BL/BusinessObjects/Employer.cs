using System;
using EMX.JobyJobs.DAL.Models;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  /// <summary>
  /// Represents a employer service class.
  /// </summary>
  public class Employer
  {
    public int EmployerId { get; set; }
    public Guid EmployerUID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string ContantPersonName { get; set; }
    public string ContactPersonPhoneNumber { get; set; }
    public string EmployerHP { get; set; }
    public DateTime JoinDate { get; set; }
    public string About { get; set; }
    public string WebSiteUrl { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Active { get; set; }
    /// <summary>
    /// The employer logo image file
    /// Note: relative path to image (from server 'EmployerLogos' dir)
    /// </summary>
    public string Logo { get; set; }


    public Employer()
    {

    }

    public Employer(int employerId, Guid employerUID, string name, string address, string phoneNumber, string contantPersonName, string contactPersonPhoneNumber, string employerHP, DateTime joinDate, string about, string webSiteUrl, DateTime lastUpdated, bool active, string logo)
    {
      EmployerId = employerId;
      EmployerUID = employerUID;
      Name = name;
      Address = address;
      PhoneNumber = phoneNumber;
      ContantPersonName = contantPersonName;
      ContactPersonPhoneNumber = contactPersonPhoneNumber;
      EmployerHP = employerHP;
      JoinDate = joinDate;
      About = about;
      WebSiteUrl = webSiteUrl;
      LastUpdated = lastUpdated;
      Active = active;
      Logo = logo;
    }

    public Employer CompleteCreate(employer item)
    {
      EmployerUID = new Guid(item.employer_uid);
      return this;
    }
  }
}
