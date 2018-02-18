using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.BL.BusinessObjects
{

  //Seeker Point-Of-View:::
  public class ConversationMessagesSeekerOverview
  {
    public int SeekerId { get; set; }
    public Dictionary<int, ConversationMessagesSeekerOverviewRecord> Chats { get; set; } = new Dictionary<int, ConversationMessagesSeekerOverviewRecord>();

    public ConversationMessagesSeekerOverview()
    {

    }


    public ConversationMessagesSeekerOverview(int seekerId)
    {
      SeekerId = seekerId;
    }

    public ConversationMessagesSeekerOverview(int seekerId, Dictionary<int, ConversationMessagesSeekerOverviewRecord> chats)
    {
      SeekerId = seekerId;
      Chats = chats;
    }
  }

  public class ConversationMessagesSeekerOverviewRecord
  {
    public int EmployerId { get; set; }
    public bool IsUnassigned { get; set; }
    public int EmployerPersonId { get; set; }
    public Dictionary<int, ChatMessage> Messages { get; set; } = new Dictionary<int, ChatMessage>();

    public ConversationMessagesSeekerOverviewRecord()
    {

    }

    public ConversationMessagesSeekerOverviewRecord(int employerId, bool isUnassigned, int employerPersonId, Dictionary<int, ChatMessage> messages)
    {
      EmployerId = employerId;
      IsUnassigned = isUnassigned;
      EmployerPersonId = employerPersonId;
      Messages = messages;
    }
  }





  //Employer Point-Of-View:::
  public class ConversationMessagesEmployerOverview
  {
    public int EmployerPersonId { get; set; }
    public Dictionary<int, ConversationMessagesEmployerOverviewRecord> Chats { get; set; } = new Dictionary<int, ConversationMessagesEmployerOverviewRecord>();

    public ConversationMessagesEmployerOverview()
    {

    }
    public ConversationMessagesEmployerOverview(int employerPersonId)
    {
      EmployerPersonId = employerPersonId;
      }
    public ConversationMessagesEmployerOverview(int employerPersonId, Dictionary<int, ConversationMessagesEmployerOverviewRecord> chats)
    {
      EmployerPersonId = employerPersonId;
      Chats = chats;
    }
  }


  public class ConversationMessagesEmployerOverviewRecord
  {
    public int SeekerId { get; set; }
    public Dictionary<int, ChatMessage> Messages { get; set; } = new Dictionary<int, ChatMessage>();

    public ConversationMessagesEmployerOverviewRecord()
    {

    }
    public ConversationMessagesEmployerOverviewRecord(int seekerId)
    {
      SeekerId = seekerId;
     }

    public ConversationMessagesEmployerOverviewRecord(int seekerId, Dictionary<int, ChatMessage> messages)
    {
      SeekerId = seekerId;
      Messages = messages;
    }
  }

}
