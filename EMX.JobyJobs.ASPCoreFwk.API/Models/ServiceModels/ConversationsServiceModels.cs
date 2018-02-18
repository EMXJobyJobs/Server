using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{

  //Seeker Point-Of-View :::

  public class SendMessageSeekerToEmployerServiceRequest
  {
    public int SeekerId { get; set; }
    public int EmployerId { get; set; }
    public bool IsUnassigned { get; set; }
    public int? EmployerPersonId { get; set; }  //if known
    public string Content { get; set; }
  }


  public class GetConversationMessagesSeekerServiceRequest
  {
    public int SeekerId { get; set; }
    public int EmployerPersonId { get; set; }
  }


  //Employer Point-Of-View :::
  public class SendMessageEmployerToSeekerServiceRequest
  {
    public int EmployerId { get; set; }
    public int EmployerPersonId { get; set; }
    public int SeekerId { get; set; }
    public string Content { get; set; }
  }

  public class GetConversationMessagesEmployerServiceRequest
  {
    public int EmployerPersonId { get; set; }
    public int SeekerId { get; set; }
  }

  public class GetEmployerUnassignedMessagesServiceRequest
  {
    public int EmployerId { get; set; }
    //    public int RequestEmployerPersonId { get; set; }
  }

  public class AssignEmployerMessageServiceRequest
  {
    public int MessageId { get; set; }
    public int EmployerPersonId { get; set; }
  }

}
