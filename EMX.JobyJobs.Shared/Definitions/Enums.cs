using System.ComponentModel;

namespace EMX.JobyJobs.Shared.Definitions
{
  public class Enums
  {

    //General:
    /// <summary>
    /// General invite status.
    /// </summary>
    public enum InviteStatus
    {
      Unspecified = 0,
      Sent = 1,
      Opened = 2,
      Accepted = 3,
      Rejected = 4,
      Cancelled = 5
    }


    //Modules:
    public enum UserType
    {
      Unspecified = 0,
      /// <summary>
      /// The Job Seeker
      /// </summary>
      [Description("מחפש עבודה")]
      Seeker = 1,
      /// <summary>
      /// The Employer
      /// </summary>
      [Description("מעסיק")]
      Employer = 2,
      /// <summary>
      /// The administrator
      /// </summary>
      [Description("מנהל מערכת")]
      Admin = 3,
      /// <summary>
      /// A public visitor (not logged in)
      /// </summary>
      [Description("פרופיל ציבורי")]
      PublicVisitor = 4
    }

    public enum UserRoles   //same as UserType
    {
      Unspecified = 0,
      Seeker = 1,
      Employer = 2,
      Administrator = 3
    }
    public enum ApplicationStatuses
    {
      Unspecified = 0,
      [Description("חדש")]
      New = 1,
      /// <summary>
      /// Note: rejected or not intersted before the phone interview.
      /// </summary>
      [Description("לא רלוונטי")]
      NotRelevant = 2,
      [Description("לאחר שיחה טלפונית")]
      AfterPhoneInterview = 3,
      [Description("זומן לראיון")]
      InvitedForInterview = 4,
      [Description("לא התקבל")]
      Rejected = 5,
      [Description("מחפש העבודה לא מעוניין")]
      NotInterested = 6,
      [Description("בתהליך לאחר ראיון")]
      InProcessAfterInterview = 7,
      [Description("נקלט לחברה")]
      Hired = 8,
    }

    public enum PositionStatuses
    {
      Unspecified = 0,
      Draft = 1,
      OnAir = 2,
      Frozen = 3,
      Archived = 4,

      /// <summary>
      /// for api purposes.
      /// </summary>
      Unarchived = 999997,
      /// <summary>
      /// for api purposes.
      /// </summary>
      Unfrozen = 999998,
      /// <summary>
      /// for search purposes.
      /// </summary>
      All = 999999
    }

    public enum PositionTypes
    {
      Unspecified = 0,
      FullTime = 1,
      PartTime = 2,
      OnShifts = 3
    }

    public enum ContactUsSubject
    {
      Unspecified = 0,
      Billing = 1,
      TechnicalError = 2,
      General = 3
    }

    public enum ReactionTypes
    {
      Unspecifed = 0,
      /// <summary>
      /// Seeker likes a employer.
      /// </summary>
      SeekerLikesEmployer = 1,   //NOT USED;
      /// <summary>
      /// Employer Likes a seeker
      /// </summary>
      EmployerLikesSeeker = 2,    //NOT USED;
      /// <summary>
      /// Seeker Likes position.
      /// </summary>
      SeekerLikesPosition = 3,    //***
      /// <summary>
      /// Employer Person Likes a seeker
      /// </summary>
      EmployerPersonLikesSeeker = 4,    //NOT USED;
      /// <summary>
      /// Employer Person Likes a seeker as a candidate for a position.
      /// </summary>
      EmployerPersonLikesSeekerAndPosition = 5,    //***
      /// <summary>
      /// a mutual like match between an employer and a seeker.
      /// </summary>
      EmployerSeekerMatch = 99991,    //NOT USED;
      /// <summary>
      /// a mutual like match between an employer person and a seeker.
      /// </summary>
      EmployerPersonSeekerMatch = 99992,    //NOT USED;
      /// <summary>
      /// a mutual like match between an employer(person) and a seeker as a candidate for a position.
      /// </summary>
      EmployerPersonSeekerPositionMatch = 99993,    //NOT USED;
      /// <summary>
      /// Reaction is disabled.
      /// </summary>
      Disabled = 99999
    }

    public enum Gender
    {
      Unspecified = 0,
      Male = 1,
      Female = 2
    }

    public enum ChatMessageType
    {
      Unspecified = 0,
      /// <summary>
      /// Seekers sends a message to an employer (or employer person)
      /// </summary>
      SeekerToEmployer = 1,
      /// <summary>
      /// Seekers sends a message to an employer (or employer person) for a specific position
      /// </summary>
      SeekerToEmployerForPosition = 2,    //NOT USED;
      /// <summary>
      /// Employer person sends a message to a seeker
      /// </summary>
      EmployerToSeeker = 3,
      /// <summary>
      /// Employer person sends a message to a seeker for a specific position.
      /// </summary>
      EmployerToSeekerForPosition = 4,    //NOT USED;
    }

    public enum EmployerPersonInviteStatuses
    {
      Unspecified = 0,
      /// <summary>
      /// Invitation was issued by the sender.
      /// </summary>
      Sent = 1,
      /// <summary>
      /// The recipient has opened his invitation and not yet replied.
      /// </summary>
      Opened = 2,
      /// <summary>
      /// Invitation was accepted by the recipient and he has completed his registration.
      /// </summary>
      Accepted = 3,
      /// <summary>
      /// Invitation was rejected by the recipient.
      /// </summary>
      Rejected = 4,
      /// <summary>
      /// Invitation was cancelled by the sender.
      /// </summary>
      Cancelled = 5
    }

    public enum KnownFields
    {
      Unspecified = 0,
      Software = 1,
      Restaurants = 2,
      Sales = 3
    }

    public enum KnownProfessions
    {
      Unspecified = 0,
      /*Software*/
      SoftwareServerSide = 1001,
      SoftwareClientSide = 1002,
      /*Restaurants*/
      RestaurantsWaiter = 1003,
      RestaurantsBarTender = 1004,
      /*Sales*/
      SalesPhone = 1005,
      SalesFrontal = 1006
    }

    public enum AppLanguages
    {
      Unspecified = 0,
      Hebrew = 1,
      English = 2,
      Russian = 3
    }

    public enum SeekerWorkStates
    {
      Unspecified = 0,
      /// <summary>
      /// Available and looking for a job.
      /// </summary>
      Available = 1,
      /// <summary>
      /// Temporarily working and looking for a job.
      /// </summary>
      TempWorking = 2,
      /// <summary>
      /// Permanently working and looking for a job.
      /// </summary>
      WorkingAndLooking = 3,
      /// <summary>
      /// Working and not looking for a job.
      /// </summary>
      WorkingNoLooking = 4,
      /// <summary>
      /// Not currently working, yet not interested in a new job.
      /// </summary>
      IdleNoLooking = 5,
      /// <summary>
      /// Generally not intersted in a new job.
      /// </summary>
      NotInterested = 6,
      /// <summary>
      /// Generally not intersted in a new job and asked not to harass him.
      /// </summary>
      NotInterestedEver = 7,
      /// <summary>
      /// The worker's work status is unknown.
      /// </summary>
      Unknown = 8
    }

    public enum InterviewInviteStatus   //NOT CURRENTLY USED
    {
      Unspecified = 0,
      Sent = 1,
      Opened = 2,
      Accepted = 3,
      Rejected = 4,
      Cancelled = 5,
      /// <summary>
      /// The interview was postponed, therefore there should be another interview record with the new details.
      /// </summary>
      Postponed = 6
    }

    public enum NotificationTypes
    {
      Unspecified = 0,
      Email = 1,
      SMS = 2,
      /// <summary>
      /// Native
      /// </summary>
      NativePush = 3
    }

    public enum PositionRanks
    {
      Unspecified = 0,
      Normal = 1,
      AboveNormal = 2,
      MidHigh = 3,
      High= 4,
    }
  }
}
