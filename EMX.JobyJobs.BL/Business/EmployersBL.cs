using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

  public interface IEmployersBL : IBusiness
  {
    void RegisterEmployer(EmployerPerson employerPerson);
    void InviteEmployerPerson(int employerPersonId, string recipientEmail);
    EmployerPersonInvite GetRegisterInvite(Guid inviteUid);
    void OnRegisterInviteReplied(Guid inviteUid, Enums.InviteStatus status);
    void RegisterEmployerPerson(Guid inviteUID, EmployerPerson newEmployerPerson);
    void OnRegisterInviteCancelled(Guid inviteUid);
    void UpdateEmployer(int employerId, Employer employer);
    void UpdateEmployerPerson(int employerPersonId, EmployerPerson employerPerson);
    void SendForgotPasswordMailEmployer(string dataEmail);
    void ForgotPasswordResetEmployer(string dataEmail, string dataPassword, string dataCode);
    void SendVerifyMailEmployer(string dataEmail);
    void VerifyEmailConfirmEmployer(string dataIdentityUserId, string dataCode);
  }

  /// <summary>
  /// Handles all business-logic activities around employers, employer persons etc.
  /// </summary>
  public class EmployersBL : IEmployersBL
  {
    private IServiceProvider _sp;

    public EmployersBL(IServiceProvider sp)
    {
      this._sp = sp;
    }

    /// <summary>
    /// Registers a new employer and also its initiator employer person.
    /// </summary>
    /// <param name="employerPerson"></param>
    public void RegisterEmployer(EmployerPerson employerPerson)
    {
      using (var db = new JobyJobsDB2())
      {
        //adds the user (under same transaction).
        Guid employerUID = employerPerson.Employer.EmployerUID != Guid.Empty ? employerPerson.Employer.EmployerUID : Guid.NewGuid();
        string identityUserId = employerPerson.Identity_UserID ?? Guid.NewGuid().ToString();
        var dbUser = new user()
        {
          Id = identityUserId,
          UserName = employerPerson.Email,   //user as email.
          PasswordHash = employerPerson.PasswordHash,
          Email = employerPerson.Email,
          PhoneNumber = employerPerson.PhoneNumber
        };
        _sp.GetService<IUsersBL>().AddUser(dbUser, Enums.UserRoles.Employer, db);

        //adds the employer (under same transaction).
        var employer = employerPerson.Employer;
        employer.EmployerUID = employerUID;
        employer.JoinDate = DateTime.Now;
        employer.Active = true;
        db.employers.Add(employer.ToDB());

        //adds the employer person.
        employerPerson.Identity_UserID = identityUserId;
        employerPerson.RegisterDate = DateTime.Now;
        employerPerson.Active = true;
        var dbEmployerPerson = employerPerson.ToDB();
        db.employer_persons.Add(dbEmployerPerson);

        //saves all work.
        db.SaveChanges();
      }
    }

    /// <summary>
    /// Issue an invitation to another employer person.
    /// Note: Sender side.
    /// </summary>
    /// <param name="employerPersonId"></param>
    /// <param name="recipientEmail"></param>
    public void InviteEmployerPerson(int employerPersonId, string recipientEmail)
    {
      //gets the sender employer person's email from db.
      string senderEmail = null;
      using (var db = new JobyJobsDB2())
      {
        employer_persons senderEmployerPerson = db.employer_persons.First(item => item.employer_person_id == employerPersonId);
        senderEmail = senderEmployerPerson.email;
      }

      //perform the actual sending of the mail.
      _sp.GetService<IEmailSender>().Send(new MailMessage(senderEmail, recipientEmail, $"Invite to JobyJobs", "Hi, You're invited to join our system, your link is http://tinyurl.com/dfkodskfodfko"));   //todo

      //adds an invitation to the db.
      using (var db = new JobyJobsDB2())
      {
        db.employer_persons_invites.Add(new employer_persons_invites()
        {
          invite_uid = Guid.NewGuid().ToString(),
          employer_person_id = employerPersonId,
          date = DateTime.Now,
          recipient_email = recipientEmail,
          status_id = (int)Enums.EmployerPersonInviteStatuses.Sent
        });   //Todo. add. transaction scoping.
        db.SaveChanges();
      }
    }


    /// <summary>
    /// Note: Recipient side.
    /// </summary>
    /// <param name="inviteUid"></param>
    public EmployerPersonInvite GetRegisterInvite(Guid inviteUid)
    {
      using (var db = new JobyJobsDB2())
      {
        //gets the invite from db.
        var invite = db.employer_persons_invites.Single(item => item.invite_uid == inviteUid.ToString());

        //update its status.
        invite.status_id = (int)Enums.EmployerPersonInviteStatuses.Opened;

        //save changes to db.
        db.SaveChanges();

        //returns the invite.
        return invite.ToBusiness();
      }
    }

    /// <summary>
    /// Note: Recipient side.
    /// </summary>
    /// <param name="inviteUid"></param>
    /// <param name="status"></param>
    public void OnRegisterInviteReplied(Guid inviteUid, Enums.InviteStatus status)
    {
      using (var db = new JobyJobsDB2())
      {
        //gets the invite from db.
        var invite = db.employer_persons_invites.Single(item => item.invite_uid == inviteUid.ToString());

        //update its status.
        invite.status_id = (int)(status == Enums.InviteStatus.Accepted
                            ? Enums.EmployerPersonInviteStatuses.Accepted
                            : Enums.EmployerPersonInviteStatuses.Rejected);

        //save changes to db.
        db.SaveChanges();
      }
    }


    /// <summary>
    /// Registers a new employer person and assigns them to an existing employer.
    /// Note: Recipient side.
    /// </summary>
    /// <param name="employerPerson"></param>
    public void RegisterEmployerPerson(Guid inviteUID, EmployerPerson employerPerson)
    {
      using (var db = new JobyJobsDB2())
      {
        //pulls out the employerId from the invitation object.
        employer_persons_invites inviteObj = db.employer_persons_invites.Single(item => item.invite_uid == inviteUID.ToString());
        int employerId = inviteObj.employer_persons.employer_id;

        string identityUserId = employerPerson.Identity_UserID ?? Guid.NewGuid().ToString();
        var dbUser = new user()
        {
          Id = identityUserId,
          UserName = employerPerson.Email,   //user as email.
          PasswordHash = employerPerson.PasswordHash,
          Email = employerPerson.Email,
          PhoneNumber = employerPerson.PhoneNumber
        };

        //update its status to accepted.
        inviteObj.status_id = (int)Enums.EmployerPersonInviteStatuses.Accepted;
        db.Entry(inviteObj).State = EntityState.Modified;

        //adds the user (under same transaction).
        _sp.GetService<IUsersBL>().AddUser(dbUser, Enums.UserRoles.Employer, db);

        //sets the employerId of the employer person.
        employerPerson.EmployeryId = employerId;

        //adds the employer person.
        var dataTimeNow = DateTime.Now;
        employerPerson.Identity_UserID = identityUserId;
        employerPerson.RegisterDate = dataTimeNow;
        employerPerson.Active = true;
        var dbEmployerPerson = employerPerson.ToDB();
        db.employer_persons.Add(dbEmployerPerson);

        //saves all work.
        db.SaveChanges();
      }
    }


    /// <summary>
    /// Note: sender side.
    /// </summary>
    /// <param name="inviteUid"></param>
    public void OnRegisterInviteCancelled(Guid inviteUid)
    {
      using (var db = new JobyJobsDB2())
      {
        //gets the invite from db.
        var invite = db.employer_persons_invites.Single(item => item.invite_uid == inviteUid.ToString());

        //update its status.
        invite.status_id = (int)Enums.EmployerPersonInviteStatuses.Cancelled;

        //save changes to db.
        db.SaveChanges();
      }
    }

    public void UpdateEmployer(int employerId, Employer employer)
    {
      using (var db = new JobyJobsDB2())
      {
        employer dbemployer;
        if (!db.employers.GetAny(item => item.employer_id == employerId, out dbemployer))
        {
          throw new JobyJobsException("The Requested application was not found");
        }

        //dbemployer.last_updated = DateTime.Now;  
        db.Entry(dbemployer.DBUpdateFrom(employer)).State = EntityState.Modified;

        db.SaveChanges();
      }
    }

    public void UpdateEmployerPerson(int employerPersonId, EmployerPerson employerPerson)
    {
      using (var db = new JobyJobsDB2())
      {
        employer_persons dbEmployerPerson;
        if (!db.employer_persons.GetAny(item => item.employer_person_id == employerPersonId, out dbEmployerPerson))
        {
          throw new JobyJobsException("The Requested application was not found");
        }

        db.Entry(dbEmployerPerson.DBUpdateFrom(employerPerson)).State = EntityState.Modified;

        db.SaveChanges();
      }
    }


    public void SendVerifyMailEmployer(string email)
    {
      //generate verify email token
      Guid securityStamp = Guid.NewGuid();

      //saves the token to db.
      string userId;
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.employer_persons.Where(item => item.user.Email == email).Select(item => item.user).Single();
        userId = dbUser.Id;
        dbUser.SecurityStamp = securityStamp.ToString();

        db.SetAsModifiedAndSave(dbUser);
      }

      //generate link and send it to user's mail.
      string link = _sp.GetService<ILinkGenerator>().GenerateVerifyEmailLinkEmployer(userId, securityStamp);     //Todo ori. put under one transaction mail+db
      _sp.GetService<IEmailSender>().Send(new MailMessage(Consts.JobyJobsSenderEmail, email, "Vertify Email", "Please click below link to verify your email: <a href=" + link + "></a>"));
    }

    public void VerifyEmailConfirmEmployer(string identityUserId, string securityStamp)
    {
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.employer_persons.Where(item => item.identity_user_id == identityUserId && item.user.SecurityStamp == securityStamp).Select(item => item.user).Single();
        dbUser.EmailConfirmed = true;
        dbUser.SecurityStamp = null;

        db.SetAsModifiedAndSave(dbUser);
      }
    }

    public void SendForgotPasswordMailEmployer(string email)   //employer: forgot password step:1
    {
      //generate forgot password token
      Guid securityStamp = Guid.NewGuid();

      //saves the token to db.
      string userId;
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.employer_persons.Where(item => item.user.Email == email).Select(item => item.user).Single();
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
      string link = _sp.GetService<ILinkGenerator>().GenerateForgotPasswordLinkEmployer(userId, securityStamp);     //Todo ori. put under one transaction mail+db
      _sp.GetService<IEmailSender>().Send(new MailMessage(Consts.JobyJobsSenderEmail, email, "Forgot Password", "Please click below link to reset you password: <a href=" + link + "></a>"));
    }


    public void ForgotPasswordResetEmployer(string email, string newPasswordHash, string securityStamp)     //employer: forgot password step:2
    {
      using (var db = new JobyJobsDB2())
      {
        var dbUser = db.employer_persons.Where(item => item.user.Email == email && item.user.SecurityStamp == securityStamp).Select(item => item.user).Single();
        dbUser.PasswordHash = newPasswordHash;

        db.SetAsModifiedAndSave(dbUser);
      }
    }

    #region Excluded code. old code.
    ///// <summary>
    ///// adds a new user to the db.
    ///// </summary>
    ///// <param name="employer"></param>
    //private   void AppendNewEmployer(Employer employer)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    db.employers.Add(employer.ToDB());
    //  }
    //}
    #endregion

    //public   void RegisterEmployerPersonQuick(EmployerPerson employerPerson)  //also appends employer.
    // {
    //  using (var db = new JobyJobsDB2())
    //  {
    //    string identityUserId = Guid.NewGuid().ToString();
    //    var dbUser = new user()
    //    {
    //      Id = identityUserId,
    //      UserName = employerPerson.Email,   //user as email.
    //      PasswordHash = employerPerson.PasswordHash,
    //      Email = employerPerson.Email,
    //      PhoneNumber = employerPerson.PhoneNumber
    //    };
    //    //adds the user (under same transaction).
    //    UsersBL.AddUser(dbUser, db);

    //    //adds the employer (under same transaction).
    //    AppendNewEmployer(employerPerson.Employer, db);

    //    //adds the employer person.
    //    var dbEmployerPerson = employerPerson.ToDB();
    //    dbEmployerPerson.identity_user_id = identityUserId;
    //    db.employer_persons.Add(dbEmployerPerson);

    //    //saves all work.
    //    db.SaveChanges();
    //  }
    //}

    ///// <summary>
    ///// adds a new user to the db.
    ///// </summary>
    ///// <param name="employer"></param>
    //public   void AppendNewEmployer(Employer employer)
    //{
    //  using (var db = new JobyJobsDB2())
    //  {
    //    db.employers.Add(employer.ToDB());
    //  }
    //}

    ///// <summary>
    ///// Continuous function: adds a new user to the db.
    ///// </summary>
    ///// <param name="employer"></param>
    ///// <param name="db"></param>
    //public   void AppendNewEmployer(Employer employer, JobyJobsDB2 db)
    //{
    //  db.employers.Add(employer.ToDB());
    //}



    //public   void SaveEmployer(Employer company)
    //{

    //}

  }


}
