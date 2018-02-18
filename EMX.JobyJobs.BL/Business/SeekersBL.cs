using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.ProxyServices.Managers;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Exceptions;
using EMX.JobyJobs.Shared.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace EMX.JobyJobs.BL.Business
{
  public interface ISeekersBL : IBusiness
  {
    CRUD<seeker> Crud { get; }
    void RegisterSeeker(Seeker seeker);
    List<Seeker> SearchSeeker(SeekersSearchCriteria criteria);
    void UpdateSeeker(int seekerId, Seeker seeker);
    void UpdateSeekerWorkState(int seekerId, Enums.SeekerWorkStates newState);

    Seeker GetSeeker(int seekerId);
    Seeker GetSeekerInternal(int seekerId, JobyJobsDB2 db);
    void SendForgotPasswordMailSeeker(string email);
    void ForgotPasswordResetSeeker(string identityUserId, string email, string newPasswordHash, string securityStamp);
    void SendVerifyMailSeeker(string email);
    void VerifyEmailConfirmSeeker(string dataIdentityUserId, string dataCode);
  }

  /// <summary>
  /// Handles all business-logic activities around job seekers.
  /// Also general methods around companyPersons, adminPersons.
  /// </summary>
  public class SeekersBL : ISeekersBL
  {
    private IServiceProvider _sp;

    /// <summary>
    /// crud repository.
    /// </summary>
    public CRUD<seeker> Crud { get; private set; }

    public SeekersBL(IServiceProvider sp)
    {
      _sp = sp;
      Crud = new CRUD<seeker>();
    }

    //Seekers Point-Of-View (or all):::
    public void RegisterSeeker(Seeker seeker)
    {
      using (var db = new JobyJobsDB2())
      {
        string identityUserId = seeker.Identity_UserID ?? Guid.NewGuid().ToString();
        var dbUser = new user()
        {
          Id = identityUserId,
          UserName = seeker.Email, //user as email.
          PasswordHash = seeker.PasswordHash,
          Email = seeker.Email,
          PhoneNumber = seeker.PhoneNumber
        };
        //adds the user.
        _sp.GetService<IUsersBL>().AddUser(dbUser, Enums.UserRoles.Seeker, db);

        //adds the seeker.
        var dataTimeNow = DateTime.Now;
        seeker.Identity_UserID = identityUserId;
        seeker.RegisterDate = dataTimeNow;
        seeker.Active = true;
        var dbSeeker = seeker.ToDB();
        db.seekers.Add(dbSeeker);

        //adds resumes.
        var resumes = new seeker_resumes()
        {
          seeker_id = dbSeeker.seeker_id,
          cv_file = seeker.CV_File,
          cv_upload_date = seeker.CV_File != null ? (DateTime?)dataTimeNow : null,
          active = true
        };
        dbSeeker.seeker_resumes.Add(resumes);

        //adds job-interests.
        var jobInterests = new seeker_job_interests()
        {
          seeker_id = dbSeeker.seeker_id,
          salary_min = null,
          location_cities = null,
          distance = null
        };
        dbSeeker.seeker_job_interests.Add(jobInterests);

        //saves all work.
        db.SaveChanges();
      }
    }

    ///// <summary>
    ///// Logins a user (seeker, employer person or an admin person)
    ///// </summary>
    //public   void Login()
    //{
    //  //UsersBL.
    //}

    /// <summary>
    /// Seeker side: Searches for all seekers matching the specified criteria.
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    public List<Seeker> SearchSeeker(SeekersSearchCriteria criteria)
    {
      using (var db = new JobyJobsDB2())
      {
        string searchTxtLowered = criteria.SearchText?.ToLower() ?? null;
        string[] locationCitiesListLowered = criteria.Location?.SplitToCsv(true).ToArray() ?? new string[0];
        int? salaryMax = criteria.MaxSalaryPerMonth != null
          ? (criteria.MaxSalaryPerMonth.Value ? criteria.MaxSalary : criteria.MaxSalary / 12)
          : //convert to month
          null;
        IQueryable<seeker> query = db.seekers.Include(item0 => item0.user).Include(item0 => item0.seeker_resumes)
          .Include(item0 => item0.seeker_job_interests)
          .Where(item => //Todo ori. fix. find a better solution (birth date calc)
            ((searchTxtLowered == null || !item.seeker_resumes.Any()) || item.about_me
               .ToLower().Contains(searchTxtLowered)) && //optional:by about me text
            ((salaryMax == null || !item.seeker_job_interests.Any()) ||
             item.seeker_job_interests.FirstOrDefault().salary_min <= salaryMax) && //optional:by salary
                                                                                    //((criteria.AgeMin == null && criteria.AgeMax == null) || (DateTime.Now - item.birth_date).Days / 365 >= criteria.AgeMin && (DateTime.Now - item.birth_date).Days / 365 <= criteria.AgeMax) &&   //optional:by age
            ((!locationCitiesListLowered.Any() || !item.seeker_job_interests.Any()) ||
             locationCitiesListLowered.Any(item0 =>
               item.seeker_job_interests.FirstOrDefault().location_cities.Contains(item0))) && //optional:by location
            true /*item.status_id == (int)Enums.PositionStatuses.OnAir*/);
        var sql = query.ToString();
        var res = query.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
        return res;
      }
    }

    public void UpdateSeeker(int seekerId, Seeker seeker)
    {
      using (var db = new JobyJobsDB2())
      {
        seeker dbSeeker;
        if (!db.seekers.GetAny(item => item.seeker_id == seekerId, out dbSeeker))
        {
          throw new JobyJobsException("The Requested item was not found");
        }
        dbSeeker.first_name = seeker.FirstName;
        dbSeeker.last_name = seeker.LastName;
        dbSeeker.phone_number = seeker.PhoneNumber;
        dbSeeker.gender = (int)seeker.Gender;
        dbSeeker.id_number = seeker.IDNumber;
        dbSeeker.birth_date = seeker.BirthDate;
        dbSeeker.work_state = (int)seeker.WorkState;
        //dbSeeker.last_updated = DateTime.Now;  
        db.Entry(dbSeeker).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    /// <summary>
    /// Seeker side (both) - Returns the seeker of the given id.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    public Seeker GetSeeker(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        return GetSeekerInternal(seekerId, db);
      }
    }
    /// <summary>
    /// Continous function: Seeker side (both) - Returns the seeker of the given id.
    /// </summary>
    /// <param name="seekerId"></param>
    /// <returns></returns>
    public Seeker GetSeekerInternal(int seekerId, JobyJobsDB2 db)
    {
      return db.seekers
        .Include(item => item.user)
        .Include(item => item.seeker_resumes)
        .Include(item => item.seeker_job_interests)
        .FirstOrDefault(item => item.seeker_id == seekerId)
        .ToBusiness();
    }

    public void SendVerifyMailSeeker(string email)
    {
      //generate verify email token
      Guid securityStamp = Guid.NewGuid();

      //saves the token to db.
      string userId;
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.seekers.Where(item => item.user.Email == email).Select(item => item.user).Single();
        userId = dbUser.Id;
        dbUser.SecurityStamp = securityStamp.ToString();

        db.SetAsModifiedAndSave(dbUser);
      }

      //generate link and send it to user's mail.
      string link = _sp.GetService<ILinkGenerator>().GenerateVerifyEmailLinkSeeker(userId, securityStamp);     //Todo ori. put under one transaction mail+db
      _sp.GetService<IEmailSender>().Send(new MailMessage(Consts.JobyJobsSenderEmail, email, "Vertify Email", "Please click below link to verify your email: <a href=" + link + "></a>"));
    }

    public void VerifyEmailConfirmSeeker(string identityUserId, string securityStamp)
    {
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.seekers.Where(item => item.identity_user_id == identityUserId && item.user.SecurityStamp == securityStamp).Select(item => item.user).Single();
        dbUser.EmailConfirmed = true;
        dbUser.SecurityStamp = null;

        db.SetAsModifiedAndSave(dbUser);
      }
    }



    public void SendForgotPasswordMailSeeker(string email)   //seeker: forgot password step:1
    {
      //generate forgot password token
      Guid securityStamp = Guid.NewGuid();

      //saves the token to db.
      string userId;
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.seekers.Where(item => item.user.Email == email).Select(item => item.user).Single();
        if (!dbUser.EmailConfirmed)
        {
          return;   //do not disclose that the email not exists.
          //throw new JobyJobsException("User's email was not confirmed; unable to reset password.");
        }
        userId = dbUser.Id;
        dbUser.SecurityStamp = securityStamp.ToString();

        db.SetAsModifiedAndSave(dbUser);
      }

      //generate link and send it to user's mail.
      string link = _sp.GetService<ILinkGenerator>().GenerateForgotPasswordLinkSeeker(userId, securityStamp);     //Todo ori. put under one transaction mail+db
      _sp.GetService<IEmailSender>().Send(new MailMessage(Consts.JobyJobsSenderEmail, email, "Forgot Password", "Please click below link to reset you password: <a href=" + link + "></a>"));
    }


    public void ForgotPasswordResetSeeker(string identityUserId, string email, string newPasswordHash, string securityStamp)     //seeker: forgot password step:2
    {
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.seekers.Where(item => item.identity_user_id == identityUserId && item.user.SecurityStamp == securityStamp).Select(item => item.user).Single();
        dbUser.PasswordHash = newPasswordHash;
        dbUser.SecurityStamp = null;

        db.SetAsModifiedAndSave(dbUser);
      }
    }

    public void UpdateSeekerWorkState(int seekerId, Enums.SeekerWorkStates newState)
    {
      using (var db = new JobyJobsDB2())
      {
        seeker dbSeeker;
        if (!db.seekers.GetAny(item => item.seeker_id == seekerId, out dbSeeker))
        {
          throw new JobyJobsException("The Requested application was not found");
        }
        dbSeeker.work_state = (int)newState;
        //dbSeeker.last_updated = DateTime.Now;
        db.Entry(dbSeeker).State = EntityState.Modified;

        db.SaveChanges();
      }
    }




    //simple crud.

    /// <summary>
    /// Note: Logical delete
    /// </summary>
    /// <param name="seekerId"></param>
    public void DeleteSeeker(int seekerId)
    {
      Crud.Delete(seekerId);
    }


    //Employer Point-Of-View:::


    //Admin Point-Of-View:::


  }
}


public static class BLExtensions
{
  public static DbContext SetAsModified<TEntity>(this DbContext db, TEntity entity) where TEntity : class
  {
    db.Entry(entity).State = EntityState.Modified;
    return db;
  }
  public static void SetAsModifiedAndSave<TEntity>(this DbContext db, TEntity entity) where TEntity : class
  {
    db.Entry(entity).State = EntityState.Modified;
    db.SaveChanges();
  }
}