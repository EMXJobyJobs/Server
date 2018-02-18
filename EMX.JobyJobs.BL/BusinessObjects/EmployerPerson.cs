using System;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public class EmployerPerson   //may include employer data.
  {
    public int EmployeryId { get; set; }
    public int EmployerPersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegisterDate { get; set; }
    public string IdNumber { get; set; }
    public string JobFunction { get; set; }
    public Enums.AppLanguages Language { get; set; }
    /// <summary>
    /// Note: relative path to image (from server 'employerPersonAvatars' dir)
    /// </summary>
    public string Avatar { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Active { get; set; }
    /// <summary>
    /// Holds a value for whether the current employer person has initiated the addition of the employer to the system.
    /// </summary>
    public bool IsInitiator { get; set; }

    public Employer Employer { get; set; }

    //user identity.
    public string Identity_UserID { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public EmployerPerson()
    {

    }

    public EmployerPerson(int employeryId, int employerPersonId, string firstName, string lastName, string phoneNumber, DateTime registerDate, string idNumber, string jobFunction, Enums.AppLanguages language, DateTime lastUpdated, bool active, bool isInitiator, Employer employer, string identityUserID, string email, string passwordHash)
    {
      EmployeryId = employeryId;
      EmployerPersonId = employerPersonId;
      FirstName = firstName;
      LastName = lastName;
      PhoneNumber = phoneNumber;
      RegisterDate = registerDate;
      IdNumber = idNumber;
      JobFunction = jobFunction;
      Language = language;
      LastUpdated = lastUpdated;
      Active = active;
      IsInitiator = isInitiator;
      Employer = employer;
      Identity_UserID = identityUserID;
      Email = email;
      PasswordHash = passwordHash;
    }

    public EmployerPerson CompleteCreate(employer_persons item)
    {
      this.Language = (Enums.AppLanguages)item.lang_id.GetValueOrDefault(Consts.MainLanguageId);

      return this;
    }
  }
}
