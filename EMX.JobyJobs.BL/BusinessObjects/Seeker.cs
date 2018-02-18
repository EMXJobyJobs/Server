using System;
using System.Linq;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Helpers;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  /// <summary>
  /// Represents a job seeker service class.
  /// </summary>
  public class Seeker
  {
    public int SeekerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegisterDate { get; set; }
    public Enums.Gender Gender { get; set; }
    public string IDNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public Enums.SeekerWorkStates WorkState { get; set; }
    public Enums.AppLanguages Language { get; set; }
    public string AboutMe { get; set; }
    /// <summary>
    /// Note: relative path to image (from server 'seekerAvatars' dir)
    /// </summary>
    public string Avatar { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool Active { get; set; }

    //user identity.
    public string Identity_UserID { get; set; }   //NOT PRELIMINARY;
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    //resumes
    /// <summary>
    /// Note: relative path to file (from server 'seekerCVs' dir)
    /// </summary>
    public string CV_File { get; set; }
    public DateTime CV_UploadDate { get; set; }

    //job interests
    public int SalaryMin { get; set; }   //-1 for no specified.
    public string[] LocationCities { get; set; }  //Array.Empty for no specified.
    public int Distance { get; set; }  //NOT USED; -1 for no specified.


    public Seeker()
    {

    }

    public Seeker(int seekerId, string firstName, string lastName, string phoneNumber, DateTime registerDate, Enums.Gender gender, string idNumber, DateTime birthDate, Enums.SeekerWorkStates workState, Enums.AppLanguages language, DateTime lastUpdated, bool active, string identityUserID, string email, string passwordHash, string aboutMe, string cvFile, DateTime cvUploadDate, int salaryMin, string[] locationCities, int distance)
    {
      SeekerId = seekerId;
      FirstName = firstName;
      LastName = lastName;
      PhoneNumber = phoneNumber;
      RegisterDate = registerDate;
      Gender = gender;
      IDNumber = idNumber;
      BirthDate = birthDate;
      WorkState = workState;
      Language = language;
      LastUpdated = lastUpdated;
      Active = active;
      Identity_UserID = identityUserID;
      Email = email;
      PasswordHash = passwordHash;
      AboutMe = aboutMe;
      CV_File = cvFile;
      CV_UploadDate = cvUploadDate;
      SalaryMin = salaryMin;
      LocationCities = locationCities;
      Distance = distance;
    }

    /// <summary>
    /// Completes the creation of the current object by an under-layer class.
    /// Sets non-matching properties only.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public Seeker CompleteCreate(seeker item)
    {
      this.Gender = (Enums.Gender)item.gender;
      this.IDNumber = item.id_number;
      this.WorkState = (Enums.SeekerWorkStates)item.work_state;
      this.Language = (Enums.AppLanguages)item.lang_id.GetValueOrDefault(Consts.MainLanguageId);

      this.PhoneNumber = item.user.PhoneNumber;    //Note: take phone-number from user object
      this.Email = item.user.Email;   //Note: take email from user object
      this.PasswordHash = item.user.PasswordHash;   //Note: take email from user object

      this.CV_File = item.seeker_resumes.SingleOrDefault()?.cv_file ?? null;
      this.CV_UploadDate = item.seeker_resumes.SingleOrDefault()?.cv_upload_date ?? DateTime.MinValue;

      this.SalaryMin = item.seeker_job_interests.SingleOrDefault()?.salary_min.GetValueOrDefault(-1) ?? -1;
      this.LocationCities = item.seeker_job_interests.SingleOrDefault()?.location_cities?.SplitToCsv(true).ToArray() ?? new string[0];
      this.Distance = item.seeker_job_interests.SingleOrDefault()?.distance.GetValueOrDefault(-1) ?? -1;

      return this;
    }
  }
}
