using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.DAL.Interfaces;

namespace EMX.JobyJobs.DAL.Models
{
  public partial class JobyJobsDB2
  {
  }

  public partial class user : IDataObject
  {
    public user(string id, string email, bool emailConfirmed, string passwordHash, string securityStamp,
      string phoneNumber, bool phoneNumberConfirmed, bool twoFactorEnabled, DateTime? lockoutEndDateUtc,
      bool lockoutEnabled, int accessFailedCount, string userName)
    {
      Id = id;
      Email = email;
      EmailConfirmed = emailConfirmed;
      PasswordHash = passwordHash;
      SecurityStamp = securityStamp;
      PhoneNumber = phoneNumber;
      PhoneNumberConfirmed = phoneNumberConfirmed;
      TwoFactorEnabled = twoFactorEnabled;
      LockoutEndDateUtc = lockoutEndDateUtc;
      LockoutEnabled = lockoutEnabled;
      AccessFailedCount = accessFailedCount;
      UserName = userName;
    }
  }

  public partial class seeker : IDataObject
  {
    public seeker(int seekerID, string identityUserID, string firstName, string lastName, string phoneNumber,
      string idNumber, string email, DateTime registerDate, DateTime birthDate, int gender, int workState,
      DateTime? currentAvailabilityDate, bool active, DateTime lastUpdated)
    {
      seeker_id = seekerID;
      identity_user_id = identityUserID;
      first_name = firstName;
      last_name = lastName;
      phone_number = phoneNumber;
      id_number = idNumber;
      this.email = email;
      register_date = registerDate;
      birth_date = birthDate;
      this.gender = gender;
      work_state = workState;
      current_availability_Date = currentAvailabilityDate;
      this.active = active;
      last_updated = lastUpdated;
    }
  }

  public partial class employer : IDataObject
  {
    public employer(int employerID, string name, string address, string phoneNumber, string contactPersonName,
      string contactPersonPhoneNumber, string employer_hp, DateTime joinDate, string logo, string about,
      string websiteURL, bool active, DateTime lastUpdated)
    {
      employer_id = employerID;
      this.name = name;
      this.address = address;
      phone_number = phoneNumber;
      contact_person_name = contactPersonName;
      contact_person_phone_number = contactPersonPhoneNumber;
      this.employer_hp = employer_hp;
      join_date = joinDate;
      this.logo = logo;
      this.about = about;
      website_url = websiteURL;
      this.active = active;
      last_updated = lastUpdated;
    }
  }

  public partial class employer_persons : IDataObject
  {
    public employer_persons(int employerPersonID, string identityUserID, int employerID, int? employerDepartmentID,
      string firstName, string lastName, string idNumber, string email, string jobFunction, string phoneNumber,
      DateTime registerDate, bool isInitiator, bool active, DateTime lastUpdate,
      ICollection<conversation_messages> conversationMessages,
      ICollection<employer_person_settings> employerPersonSettings, empoyer_departments empoyerDepartments,
      employer employer, ICollection<reaction> reactions, ICollection<employer_persons_invites> employerPersonsInvites,
      user user)
    {
      employer_person_id = employerPersonID;
      identity_user_id = identityUserID;
      employer_id = employerID;
      employer_department_id = employerDepartmentID;
      first_name = firstName;
      last_name = lastName;
      id_number = idNumber;
      this.email = email;
      job_function = jobFunction;
      phone_number = phoneNumber;
      register_date = registerDate;
      is_initiator = isInitiator;
      this.active = active;
      last_update = lastUpdate;
    }
  }
  public partial class admin_persons : IDataObject
  {

  }

  public partial class position : IDataObject
  {

  }

  public partial class application : IActiveUpdateAwareDataObject
  {

    //extension properties
    /// <summary>
    /// Note: not to be used for sql translation.
    /// </summary>
    public int Id { get => id; set => id = value; }
    /// <summary>
    /// Note: not to be used for sql translation.
    /// </summary>
    public bool Active { get => active.Value; set => active = value; }
    /// <summary>
    /// Note: not to be used for sql translation.
    /// </summary>
    public DateTime LastUpdated { get => last_updated.Value; set => last_updated = value; }


    public application(int id, int seekerID, int positionID, int statusID, DateTime applicationStartDate, bool? watched, DateTime? lastUpdated, bool? active)
    {
      this.id = id;
      seeker_id = seekerID;
      position_id = positionID;
      status_id = statusID;
      application_start_date = applicationStartDate;
      this.watched = watched;
      last_updated = lastUpdated;
      this.active = active;
    }

  }

  public partial class interview : IActiveUpdateAwareDataObject
  {
    //extension properties
    /// <summary>
    /// Note: not to be used for sql translation.
    /// </summary>
    public int Id { get => interview_id; set => interview_id = value; }
    /// <summary>
    /// Note: not to be used for sql translation.
    /// </summary>
    public bool Active { get => active.Value; set => active = value; }
    /// <summary>
    /// Note: not to be used for sql translation.
    /// </summary>
    public DateTime LastUpdated { get => last_updated.Value; set => last_updated = value; }

    public interview()
    {

    }

    public interview(int interviewID, string interviewUID, int employerID, int? applicationID, int seekerID, DateTime inviteDate, int inviteStatusID, string inviteCancelRejectReason, DateTime dueDate, string location, string notes, DateTime? lastUpdated, bool? active)
    {
      interview_id = interviewID;
      interview_uid = interviewUID;
      employer_id = employerID;
      application_id = applicationID;
      seeker_id = seekerID;
      invite_date = inviteDate;
      invite_status_id = inviteStatusID;
      invite_cancelReject_reason = inviteCancelRejectReason;
      due_date = dueDate;
      this.location = location;
      this.notes = notes;
      last_updated = lastUpdated;
      this.active = active;
    }
  }

  public partial class reaction : IActiveUpdateAwareDataObject
  {
    //public bool IsDisabled => (this.reaction_disabled != null && this.reaction_disabled == true);

    //extension properties
    public int Id { get => id; set => id = value; }
    public bool Active { get => active.Value; set => active = value; }
    public DateTime LastUpdated { get => last_updated.Value; set => last_updated = value; }


    public reaction()
    {

    }

    public reaction(int id, int seekerID, int? employerID, int reactionTypeID, int? positionID, int? employerPersonID,
      bool? reactionDisabled, DateTime? lastUpdated, bool? active)
    {
      this.id = id;
      seeker_id = seekerID;
      employer_id = employerID;
      reaction_type_id = reactionTypeID;
      position_id = positionID;
      employer_person_id = employerPersonID;
      reaction_disabled = reactionDisabled;
      last_updated = lastUpdated;
      this.active = active;
    }

  }

  public partial class profession : IDataObject
  {

  }

  public partial class field : IDataObject
  {

  }
  public partial class conversation_messages : IDataObject
  {

  }

}
