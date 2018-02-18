using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.Helpers;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Helpers;

namespace EMX.JobyJobs.BL.Business
{
  public interface IUsersBL : IBusiness
  {
    string GetUserIdFromCredentialsSeeker(string userName, string password, out int seekerId);
    string GetUserIdFromCredentialsEmployer(string userName, string password, out int employerPersonId, out int employerId);
    void AddUser(user user, Enums.UserRoles role, JobyJobsDB2 db);
  }


  /// <summary>
  /// Internal class: Handles all business-logic activities around users, roles, registration, logins, 
  /// passcodes, forgot-password, collaboration with facebook, google and so on.
  /// </summary>
  public class UsersBL : IUsersBL
  {
    /// <summary>
    /// Returns the user id of the given seeker userName (email) and password(hashed 256)
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password">hashed password</param>
    /// <returns></returns>
    public string GetUserIdFromCredentialsSeeker(string userName, string password, out int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        user user;
        if (db.users.GetAny(user0 => user0.UserName == userName && user0.PasswordHash == password, out user))
        {
          seekerId = user.seekers.Single().seeker_id;
          return user.Id;
        }
        else
        {
          seekerId = 0;
          return Guid.Empty.ToString();
        }
      }
    }

    /// <summary>
    /// Returns the user id of the given employer userName (email) and password(hashed 256)
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password">hashed password</param>
    /// <returns></returns>
    public string GetUserIdFromCredentialsEmployer(string userName, string password, out int employerPersonId, out int employerId)
    {
      using (var db = new JobyJobsDB2())
      {
        user user;
        if (db.users.GetAny(user0 => user0.UserName == userName && user0.PasswordHash == password, out user))
        {
          employerPersonId = user.employer_persons.Single().employer_person_id;
          employerId = user.employer_persons.Single().employer_id;
          return user.Id;
        }
        else
        {
          employerPersonId = 0;
          employerId = user.employer_persons.Single().employer_id;
          return Guid.Empty.ToString();
        }
      }
    }

    /// <summary>
    /// Continuous function: adds a new user to the specified role in the db.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="db"></param>
    public void AddUser(user user, Enums.UserRoles role, JobyJobsDB2 db)
    {
      string roleGUID = ConstsEnumsTranslations.GetRoleGUID(role).ToString();
      var roleObj = db.roles.FirstOrDefault(item => item.Id == roleGUID);
      roleObj.users.Add(user);
    }

    //public   bool CheckUser(string email, string password, ICryptographyHelper injectCryptoHelper)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    string emailLowered = email.ToLower();
    //    string passwordLoweredHashed = password.ToLower();
    //    return db.users.Any(user => user.Email == emailLowered &&
    //             user.PasswordHash == passwordLoweredHashed);   //Assumption: email and pass kept in lower case (/lower case hashed)
    //  }
    //}

  }


}
