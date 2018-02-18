using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public class EmployerPersonInvite
  {
    public int InviteId { get; set; }
    public string InviteUID { get; set; }
    public int EmployerPersonId { get; set; }
    public string RecipientEmail { get; set; }
    public Enums.EmployerPersonInviteStatuses Status { get; set; }
    public DateTime Date { get; set; }

    public EmployerPersonInvite()
    {

    }

    public EmployerPersonInvite(int inviteId, string inviteUID, int employerPersonId, string recipientEmail, Enums.EmployerPersonInviteStatuses status, DateTime date)
    {
      InviteId = inviteId;
      InviteUID = inviteUID;
      EmployerPersonId = employerPersonId;
      RecipientEmail = recipientEmail;
      Status = status;
      Date = date;
    }

    public EmployerPersonInvite CompleteCreate(employer_persons_invites item)
    {
      InviteUID = item.invite_uid;
      Status = (Enums.EmployerPersonInviteStatuses)item.status_id;
      return this;
    }
  }
}
