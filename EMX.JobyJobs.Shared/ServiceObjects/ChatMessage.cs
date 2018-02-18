using System;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.Shared.ServiceObjects
{
  public class ChatMessage
  {
    public int MessageId { get; set; }
    public Guid MessageUId { get; set; }
    public Enums.ChatMessageType MessageType { get; set; }
    public int SeekerId { get; set; }
    public int EmployerId { get; set; }
    public bool IsUnassigned { get; set; }
    public int EmployerPersonId { get; set; }   //0 (zero) if not assigned
    public string Header { get; set; }     //not used
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }
    public bool IsArchived { get; set; }
    public bool Active { get; set; }

    public ChatMessage()
    {

    }

    public ChatMessage(int messageId, Guid messageUId, Enums.ChatMessageType messageType, int seekerId, int employerId, bool isUnassigned, int employerPersonId, string header, string content, DateTime date, bool isRead, bool isArchived, bool active)
    {
      MessageId = messageId;
      MessageUId = messageUId;
      MessageType = messageType;
      SeekerId = seekerId;
      EmployerId = employerId;
      IsUnassigned = isUnassigned;
      EmployerPersonId = employerPersonId;
      Header = header;
      Content = content;
      Date = date;
      IsRead = isRead;
      IsArchived = isArchived;
      Active = active;
    }
  }
}
