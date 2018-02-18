using System;

namespace EMX.JobyJobs.Admin.ASPCoreFwk.Models
{
  public class ErrorViewModel
  {
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
  }
}