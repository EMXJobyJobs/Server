using System;
using EMX.JobyJobs.ASPCoreFwk.API.Controllers;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.ASPCoreFwk.API.Helpers
{
  public class LinkGenerator : ILinkGenerator
  {
    public string GenerateVerifyEmailLinkSeeker(string identityUserId, Guid securityStamp, string httpScheme = "http")
    {
      string methodName = nameof(AccountController.VerifyEmailConfirmSeeker);
      return httpScheme + "://" + Consts.JobyJobsCallbackHost + "/Account/" + methodName
             + "?userId=" + identityUserId.ToString() + "?code=" + securityStamp.ToString();
    }

    public string GenerateVerifyEmailLinkEmployer(string identityUserId, Guid securityStamp, string httpScheme = "http")
    {
      string methodName = nameof(AccountController.VerifyEmailConfirmEmployer);
      return httpScheme + "://" + Consts.JobyJobsCallbackHost + "/Account/" + methodName
             + "?userId=" + identityUserId.ToString() + "?code=" + securityStamp.ToString();
    }

    public string GenerateForgotPasswordLinkSeeker(string identityUserId, Guid securityStamp, string httpScheme = "http")
    {
      string methodName = nameof(AccountController.ForgotPasswordResetSeeker);
      return httpScheme + "://" + Consts.JobyJobsCallbackHost + "/Account/" + methodName
             + "?userId=" + identityUserId.ToString() + "?code=" + securityStamp.ToString();
    }


    public string GenerateForgotPasswordLinkEmployer(string identityUserId, Guid securityStamp, string httpScheme = "http")
    {
      string methodName = nameof(AccountController.ForgotPasswordResetEmployer);
      return httpScheme + "://" + Consts.JobyJobsCallbackHost + "/Account/" + methodName
                      + "?userId=" + identityUserId.ToString() + "?code=" + securityStamp.ToString();
    }

    public string GeneratePositionShareableLink(Guid positionUID, string httpScheme = "http")
    {
      string methodName = nameof(PositionsController.GetPosition);
      return httpScheme + "://" + Consts.JobyJobsCallbackHost + "/Positions/" + methodName + "?positionUID=" + positionUID.ToString();
    }



  }
}
