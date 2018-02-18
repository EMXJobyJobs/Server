using System;
using System.ComponentModel.DataAnnotations;
using EMX.JobyJobs.Shared.Definitions;
using Microsoft.AspNetCore.Http;

namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{

  //Seeker Point-of-View:::
  public abstract class RegisterServiceRequestBase
  {
    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }    //sha-256 hashed
  }

  public class RegisterSeekerServiceRequest : RegisterServiceRequestBase
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IDNumber { get; set; }
    public DateTime BirthDate { get; set; }
    [Required]
    public Enums.Gender Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string AboutMe { get; set; }
    //public int? FieldId { get; set; }
    //public int? ProfessionId { get; set; }
    /// <summary>
    /// Note: avatar image file.
    /// </summary>
    public IFormFile SeekerAvatarFile { get; set; }
    /// <summary>
    /// Note: cv file.
    /// </summary>
    public IFormFile SeekerCVFile { get; set; }
  }

  public class SendVerifyEmailMailServiceRequest    //for both
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public SendVerifyEmailMailServiceRequest()
    {

    }

    public SendVerifyEmailMailServiceRequest(string email)
    {
      Email = email;
    }
  }


  public class VerifyEmailConfirmServiceRequest    //for both
  {
    public string UserId { get; set; }
    public string Code { get; set; }
  }


  public class SendForgotPasswordMailServiceRequest    //for both
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public SendForgotPasswordMailServiceRequest()
    {

    }

    public SendForgotPasswordMailServiceRequest(string email)
    {
      Email = email;
    }
  }

  public class ForgotPasswordServiceRequest    //for both
  {
    public string IdentityUserId { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }      //sha-256 hashed

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }      //sha-256 hashed

    public string Code { get; set; }

    public ForgotPasswordServiceRequest()
    {

    }

    public ForgotPasswordServiceRequest(string email, string password, string confirmPassword, string code)
    {
      Email = email;
      Password = password;
      ConfirmPassword = confirmPassword;
      Code = code;
    }
  }


  ///// <summary>
  //  /// Login view model
  //  /// </summary>
  //  public class LoginViewModel    //both for seeker and employer
  //{
  //  [Required]
  //  [Display(Name = "Email")]
  //  [EmailAddress]
  //  public string Email { get; set; }

  //  [Required]
  //  [DataType(DataType.Password)]
  //  [Display(Name = "Password")]
  //  public string Password { get; set; }      //sha-256 hashed

  //  [Display(Name = "Remember me?")]
  //  public bool RememberMe { get; set; }      //sha-256 hashed
  //}

  public class GenerateTokenServiceRequest //both for seeker and employer
  {
    /// <summary>
    /// User configuration: the user name.
    /// </summary>
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }

    /// <summary>
    /// User configuration: the password (hashed).
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } //sha-256 hashed

    /// <summary>
    /// Request configuration: the language code.
    /// </summary>
    public string LanguageCode { get; set; }
  }

  //public class ForgotPasswordViewModel  //forgot password page 1: both for seeker and employer
  //{
  //  [Required]
  //  [EmailAddress]
  //  [Display(Name = "Email")]
  //  public string Email { get; set; }
  //}

  //public class ResetPasswordViewModel   //forgot password page 2: both for seeker and employer
  //{
  //  [Required]
  //  [EmailAddress]
  //  [Display(Name = "Email")]
  //  public string Email { get; set; }

  //  [Required]
  //  [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
  //  [DataType(DataType.Password)]
  //  [Display(Name = "Password")]
  //  public string Password { get; set; }

  //  [DataType(DataType.Password)]
  //  [Display(Name = "Confirm password")]
  //  [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
  //  public string ConfirmPassword { get; set; }

  //  public string Code { get; set; }
  //}



  //Employer Point-of-View:::
  public class RegisterEmployerServiceRequest : RegisterEmployerPersonServiceRequest   //inherits from register employer person
  {
    [Required]
    public string EmployerName { get; set; }
    [Required]
    public string EmployerHP { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public string About { get; set; }
    public string WebSiteURL { get; set; }
    /// <summary>
    /// Note: logo image file.
    /// </summary>
    public IFormFile EmployerLogoFile { get; set; }
    /// <summary>
    /// Note: avatar image file.
    /// </summary>
    public IFormFile EmployerPersonAvatarFile { get; set; }
  }

  public class RegisterEmployerPersonServiceRequest : RegisterServiceRequestBase   //inherits from base register
  {
    /// <summary>
    /// Note: null if not relevant.
    /// </summary>
    [Required]
    public Guid? InviteUID { get; set; }
    /// <summary>
    /// Note: null if not relevant.
    /// </summary>
    [Required]
    public int? EmployeryId { get; set; }
    [Required]
    public string EmployerPersonFirstName { get; set; }
    [Required]
    public string EmployerPersonLastName { get; set; }
    [Required]
    public string EmployerPersonPhoneNumber { get; set; }
    [Required]
    public string EmployerPersonIdNumber { get; set; }
    [Required]
    public string EmployerPersonJobFunction { get; set; }
    /// <summary>
    /// Note: avatar image file.
    /// </summary>
    public IFormFile EmployerPersonAvatarFile { get; set; }
  }


  public class InviteEmployerPersonServiceRequest
  {
    /// <summary>
    /// The sender employer person id.
    /// </summary>
    [Required]
    public int SenderEmployerPersonId { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string RecipientEmail { get; set; }
  }


  //Admin Point-Of-View:::
  //public class RegisterAdminPersonViewModel : RegisterViewModelBase
  //{
  //  public string FirstName { get; set; }
  //  public string LastName { get; set; }
  //}
}
