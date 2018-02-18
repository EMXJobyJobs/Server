using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using EMX.JobyJobs.ASPCoreFwk.API.Helpers;
using EMX.JobyJobs.ASPCoreFwk.API.Models;
using EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels;
using EMX.JobyJobs.BL.Business;
using EMX.JobyJobs.Shared.Definitions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace EMX.JobyJobs.ASPCoreFwk.API.Controllers
{
  /// <summary>
  /// Handles all user account requests for both seeker and employer sides, including registration, log-in, send codes, forgot-password, and verify emails.
  /// </summary>
  [AllowAnonymous]
  public class AccountController : Controller
  {
    private IConfiguration _configuration;
    private IServiceProvider _sp;
    private ISeekersBL _seekersBL;
    private IEmployersBL _employersBL;

    public AccountController(IConfiguration configuration, IServiceProvider sp, ISeekersBL seekersBL, IEmployersBL employersBL)
    {
      this._configuration = configuration;
      this._sp = sp;
      this._seekersBL = seekersBL;
      this._employersBL = employersBL;
    }


    //Seeker Point-Of-View:::
    // GET: /<controller>/
    public IActionResult Index()
    {
      return View();
    }

    //
    // API POST: /Account/RegisterSeeker
    [HttpPost]
    public ActionResult RegisterSeeker(RegisterSeekerServiceRequest data)
    {
      if (ModelState.IsValid)
      {
        var bscSeeker = data.ToBusiness();
        bscSeeker.Identity_UserID = Guid.NewGuid().ToString();
        bscSeeker.Avatar = data.SeekerAvatarFile != null ? this.SaveUploadedSeekerAvatarFile(data.SeekerAvatarFile, bscSeeker.Identity_UserID) : null;
        bscSeeker.CV_File = data.SeekerCVFile != null ? this.SaveUploadedSeekerCVFile(data.SeekerCVFile, bscSeeker.Identity_UserID) : null;
        _sp.GetService<ISeekersBL>().RegisterSeeker(bscSeeker);
        return Ok();
      }
      return BadRequest(ModelState);
    }


    /// <summary>
    /// Generates a token with the given user and request configuration.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult GenerateTokenSeeker(GenerateTokenServiceRequest data)
    {
      if (ModelState.IsValid)
      {
        //This method returns user id from username and password.
        int seekerId;
        var userId = _sp.GetService<IUsersBL>().GetUserIdFromCredentialsSeeker(data.UserName, data.Password, out seekerId);
        if (userId == Guid.Empty.ToString())
        {
          return Unauthorized();
        }

        Claim[] claims = new[]
        {
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),   //jwt token identifier
          new Claim(JwtRegisteredClaimNames.Sub, data.UserName) ,  //subject= username(email)
          new Claim(ClaimTypes.Name, seekerId.ToString()) ,  //identityName=seekerId
          new Claim(ClaimTypes.NameIdentifier, userId) ,  //subject
          new Claim(ClaimTypes.Role, Enums.UserRoles.Seeker.ToString()),   //role
          new Claim(ClaimTypes.Locality, data.LanguageCode.ToString())   //language
        };

        var token = new JwtSecurityToken
        (
          issuer: _configuration.Get<AppSettings>().JWT.JwtIssuer,
          //audience: _configuration["Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddDays(_configuration.Get<AppSettings>().JWT.JwtExpireDays),
          notBefore: DateTime.UtcNow,
          signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Get<AppSettings>().JWT.JwtSigningKey)),
            SecurityAlgorithms.HmacSha256)
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
      }

      return BadRequest();
    }

    [HttpGet]
    public IActionResult SendVerifyMailSeeker(SendVerifyEmailMailServiceRequest data)   //seeker: verify email : step1
    {
      _seekersBL.SendVerifyMailSeeker(data.Email);
      return Ok();
    }

    [HttpGet]

    public IActionResult VerifyEmailConfirmSeeker(VerifyEmailConfirmServiceRequest data)   //seeker: verify email : step2
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _seekersBL.VerifyEmailConfirmSeeker(data.UserId, data.Code);
      return Ok();
    }

    [HttpGet]
    public IActionResult SendForgotPasswordMailSeeker(SendForgotPasswordMailServiceRequest data)   //seeker: forgot password: step1
    {
      _seekersBL.SendForgotPasswordMailSeeker(data.Email);
      return Ok();
    }

    [HttpGet]
    public IActionResult ForgotPasswordResetSeeker(ForgotPasswordServiceRequest data)   //seeker: forgot password: step2
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _seekersBL.ForgotPasswordResetSeeker(data.IdentityUserId, data.Email, data.Password, data.Code);
      return Ok();
    }




    //Employer Point-Of-View:::



    // POST: /Account/RegisterEmployer
    /// <summary>
    /// Registers a new employer and also its initiator employer person (Step 1).
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult RegisterEmployer(RegisterEmployerServiceRequest data)
    {
      if (ModelState.IsValid)
      {
        var bscEmployerPerson = data.ToBusiness();
        bscEmployerPerson.Identity_UserID = Guid.NewGuid().ToString();
        bscEmployerPerson.Avatar = data.EmployerPersonAvatarFile != null ? this.SaveUploadedEmployerPersonAvatarFile(data.EmployerPersonAvatarFile, bscEmployerPerson.Identity_UserID) : null;
        bscEmployerPerson.Employer.EmployerUID = Guid.NewGuid();
        bscEmployerPerson.Employer.Logo = data.EmployerLogoFile != null ? this.SaveUploadedEmployerLogoFile(data.EmployerLogoFile, bscEmployerPerson.Employer.EmployerUID) : null;
        _sp.GetService<IEmployersBL>().RegisterEmployer(bscEmployerPerson);
        return Ok();
      }
      return BadRequest(ModelState);
    }


    /// <summary>
    /// Issue an invitation to another employer person (Step 2).
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = "Employer")]
    public IActionResult InviteEmployerPerson(InviteEmployerPersonServiceRequest model)
    {
      if (ModelState.IsValid)
      {
        _sp.GetService<IEmployersBL>().InviteEmployerPerson(model.SenderEmployerPersonId, model.RecipientEmail);
        return Ok();
      }
      return BadRequest(ModelState);
    }

    /// <summary>
    /// Employer side- Returns the register invite and marks it as opened.
    /// Note: recipient side (Step 3).
    /// </summary>
    /// <param name="inviteUID"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult GetRegisterInvite(Guid inviteUID)
    {
      return Json(_sp.GetService<IEmployersBL>().GetRegisterInvite(inviteUID));
    }
    ///// <summary>
    ///// Note: recipient side.
    ///// </summary>
    ///// <param name="inviteUID"></param>
    ///// <returns></returns>
    //private IActionResult OpenRegisterInviteDialog()
    //{
    //  return View();
    //}


    // POST: /Account/RegisterEmployerPerson
    /// <summary>
    /// Registers a new employer person and assigns them to an existing employer (Step 4A).
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult RegisterEmployerPerson(RegisterEmployerPersonServiceRequest data)
    {
      if (ModelState.IsValid)
      {
        var bscEmployerPerson = data.ToBusiness();
        bscEmployerPerson.Identity_UserID = Guid.NewGuid().ToString();
        bscEmployerPerson.Avatar = data.EmployerPersonAvatarFile != null ? this.SaveUploadedEmployerPersonAvatarFile(data.EmployerPersonAvatarFile, bscEmployerPerson.Identity_UserID) : null;
        _sp.GetService<IEmployersBL>().RegisterEmployerPerson(data.InviteUID.Value, bscEmployerPerson);
        return Ok();
      }
      return BadRequest(ModelState);
    }

    /// <summary>
    /// Note: recipient side (Step 4B).
    /// </summary>
    /// <param name="inviteUID"></param>
    /// <returns></returns>

    [HttpPost]
    public IActionResult OnRegisterInviteRejected(Guid inviteUID)
    {
      _sp.GetService<IEmployersBL>().OnRegisterInviteReplied(inviteUID, Enums.InviteStatus.Rejected);
      return Ok();
    }

    /// <summary>
    /// Note: sender side.
    /// </summary>
    /// <param name="inviteUID"></param>
    /// <returns></returns>
    [Authorize(Roles = "Employer")]
    [HttpPost]
    public IActionResult OnRegisterInviteCancelled(Guid inviteUID)
    {
      _sp.GetService<IEmployersBL>().OnRegisterInviteCancelled(inviteUID);
      return Ok();
    }


    /// <summary>
    /// <summary>
    /// Generates a token with the given user and request configuration.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult GenerateTokenEmployer(GenerateTokenServiceRequest data)
    {
      if (ModelState.IsValid)
      {
        //This method returns user id from username and password.
        int employerPersonId;
        int employerId;
        var userId = _sp.GetService<IUsersBL>().GetUserIdFromCredentialsEmployer(data.UserName, data.Password, out employerPersonId, out employerId);
        if (userId == Guid.Empty.ToString())
        {
          return Unauthorized();
        }

        Claim[] claims = new[]
        {
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),   //jwt token identifier
          new Claim(JwtRegisteredClaimNames.Sub, data.UserName) ,  //subject= username(email)
          new Claim(ClaimTypes.Name, employerPersonId.ToString()) ,  //identityName=employerPersonId
          new Claim(ClaimTypes.GroupSid, employerId.ToString()) ,  //identityName=employerId
          new Claim(ClaimTypes.NameIdentifier, userId) ,  //subject
          new Claim(ClaimTypes.Role, Enums.UserRoles.Employer.ToString())   //role
        };

        var token = new JwtSecurityToken
        (
          issuer: _configuration.Get<AppSettings>().JWT.JwtIssuer,
          //audience: _configuration["Audience"],
          claims: claims,
          expires: DateTime.UtcNow.AddDays(_configuration.Get<AppSettings>().JWT.JwtExpireDays),
          notBefore: DateTime.UtcNow,
          signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Get<AppSettings>().JWT.JwtSigningKey)),
            SecurityAlgorithms.HmacSha256)
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
      }

      return BadRequest();
    }

    [HttpGet]
    public IActionResult SendVerifyMailEmployer(SendVerifyEmailMailServiceRequest data)
    {
      _employersBL.SendVerifyMailEmployer(data.Email);
      return Ok();
    }

    [HttpGet]
    public IActionResult VerifyEmailConfirmEmployer(VerifyEmailConfirmServiceRequest data)   //employer: verify email : step1
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _employersBL.VerifyEmailConfirmEmployer(data.UserId, data.Code);
      return Ok();
    }

    [HttpGet]
    public IActionResult SendForgotPasswordMailEmployer(SendForgotPasswordMailServiceRequest data)   //seeker: forgot password: step1
    {
      _employersBL.SendForgotPasswordMailEmployer(data.Email);
      return Ok();
    }

    [HttpGet]
    public IActionResult ForgotPasswordResetEmployer(ForgotPasswordServiceRequest data)   //seeker: forgot password: step1
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _employersBL.ForgotPasswordResetEmployer(data.Email, data.Password, data.Code);
      return Ok();
    }


    /// <summary>
    /// Simulates the bearer authorization mechanism.
    /// returns the supplied string as doubled.
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    [Authorize]
    public IActionResult GetAuthTest(string value)
    {
      return Content(value + " " + value);
    }

    [HttpGet]
    public IEnumerable<string> TestGetData1()
    {
      return new string[] { "value1", "value2" };
    }

    [Authorize]
    [HttpGet]
    public IEnumerable<string> TestGetData2()
    {
      return new string[] { "value1", "value2" };
    }


    public static string TestEcho(HttpContext httpContext)
    {
      var data = ReadToEnd(httpContext.Request.Body);
      return System.Text.Encoding.Default.GetString(data);
    }


    private static byte[] ReadToEnd(System.IO.Stream stream)
    {
      long originalPosition = 0;

      if (stream.CanSeek)
      {
        originalPosition = stream.Position;
        stream.Position = 0;
      }

      try
      {
        byte[] readBuffer = new byte[4096];

        int totalBytesRead = 0;
        int bytesRead;

        while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
        {
          totalBytesRead += bytesRead;

          if (totalBytesRead == readBuffer.Length)
          {
            int nextByte = stream.ReadByte();
            if (nextByte != -1)
            {
              byte[] temp = new byte[readBuffer.Length * 2];
              Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
              Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
              readBuffer = temp;
              totalBytesRead++;
            }
          }
        }

        byte[] buffer = readBuffer;
        if (readBuffer.Length != totalBytesRead)
        {
          buffer = new byte[totalBytesRead];
          Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
        }
        return buffer;
      }
      finally
      {
        if (stream.CanSeek)
        {
          stream.Position = originalPosition;
        }
      }
    }
  }
}
