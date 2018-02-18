using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.BL.Business
{
  public interface ILinkGenerator
  {
    string GenerateVerifyEmailLinkSeeker(string identityUserId, Guid securityStamp, string httpScheme = "Http");
    string GenerateVerifyEmailLinkEmployer(string identityUserId, Guid securityStamp, string httpScheme = "Http");
    string GenerateForgotPasswordLinkSeeker(string identityUserId, Guid securityStamp, string httpScheme = "Http");
    string GenerateForgotPasswordLinkEmployer(string identityUserId, Guid securityStamp, string httpScheme = "Http");
    string GeneratePositionShareableLink(Guid positionUID, string httpScheme = "Http");
  }
}
