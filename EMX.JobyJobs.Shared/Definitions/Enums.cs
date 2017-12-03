using System.ComponentModel;

namespace EMX.JobyJobs.Shared.Definitions
{
    public class Enums
    {
        public enum UserType
        {
            Unspecified = 0,
            /// <summary>
            /// The Worker (candidate)
            /// </summary>
            [Description("מועמד")]
            Worker = 1,
            /// <summary>
            /// The Company(employer)
            /// </summary>
            [Description("מעסיק")]
            Company = 2,
            /// <summary>
            /// The administrator
            /// </summary>
            [Description("מנהל מערכת")]
            Admin = 3
        }

        public enum ApplicationStatuses
        {
            Unspecified=0,
            [Description("חדש")]
            New=1,
            [Description("לא רלוונטי")]
            NotRelevant = 2,
            [Description("לאחר שיחה טלפונית")]
            AfterPhoneInterview = 3,
            [Description("זומן לראיון")]
            InvitedForInterview = 4,
            [Description("לא התקבל")]
            Rejected = 5,
            [Description("עובד לא מעוניין")]
            NotInterested = 6,
            [Description("בתהליך לאחר ראיון")]
            InProcessAfterInterview = 7,
            [Description("נקלט לחברה")]
            Accepted = 8,
        }


        public enum ContactUsSubject
        {
            Unspecified = 0,
            Billing = 1,
            TechnicalError = 2,
            General = 3
        }
    }
}
