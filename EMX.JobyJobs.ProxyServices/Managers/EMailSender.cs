using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EMX.JobyJobs.ProxyServices.Managers
{
  public interface IEmailSender
  {
    void Send(MailMessage message);
  }
  public class EmailSender : IEmailSender
  {
    public void Send(MailMessage message)
    {

    }
  }

}
