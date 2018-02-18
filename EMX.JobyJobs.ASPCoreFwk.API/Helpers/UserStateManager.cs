using EMX.JobyJobs.ASPCoreFwk.API.Controllers;
using EMX.JobyJobs.Shared.Definitions;
using Microsoft.AspNetCore.Http;

namespace EMX.JobyJobs.ASPCoreFwk.API.Helpers
{
  public interface IUserStateManager
  {
    bool LoggedIn { get; set; }
    Enums.UserType UserMode { get; set; }
    int Identity_UserId { get; set; }
    int SeekerId { get; set; }
    int EmployerId { get; set; }
    int EmployerPersonId { get; set; }
    int AdminPersonId { get; set; }

    void SetSessionValue(HttpContext httpContext, string key, string value);
    object GetSessionValue(HttpContext httpContext, string key);
    string GetSessionValueAsString(HttpContext httpContext, string key);
  }

  /// <summary>
  /// Handles all actions around the user (session) state management.
  /// Note: This is session-scoped.
  /// </summary>
  public class UserStateManager : IUserStateManager
  {

    #region SingleTon

    private static UserStateManager _instance;

    public static UserStateManager Instance
    {
      get
      {

        if (_instance == null)
        {
          _instance = new UserStateManager();
        }
        return _instance;
      }
    }

    #endregion

    /// <summary>
    /// Holds a value for whether the user has logged in.
    /// </summary>
    public bool LoggedIn { get; set; }
    /// <summary>
    /// The user type for the current session: seeker, employer person, admin person.
    /// </summary>
    public Enums.UserType UserMode { get; set; }
    /// <summary>
    /// Holds the identity user id; applies to all user types.
    /// </summary>
    public int Identity_UserId { get; set; }
    /// <summary>
    /// only applies for seeker sessions.
    /// -1 for none.
    /// </summary>
    public int SeekerId { get; set; }
    /// <summary>
    /// only applies for employer sessions.
    /// -1 for none.
    /// </summary>
    public int EmployerId { get; set; }
    /// <summary>
    /// only applies for employer sessions.
    /// -1 for none.
    /// </summary>
    public int EmployerPersonId { get; set; }
    /// <summary>
    /// only applies for admin sessions.
    /// -1 for none.
    /// </summary>
    public int AdminPersonId { get; set; }


    private UserStateManager()
    {
      SeekerId = -1;
      EmployerId = -1;
      EmployerPersonId = -1;
      AdminPersonId = -1;
    }

    public void SetSessionValue(HttpContext httpContext, string key, string value)
    {
      httpContext.Session.SetString(key, value);
    }

    public object GetSessionValue(HttpContext httpContext, string key)
    {
      object obj = httpContext.Session.GetString(key);
      return obj;
    }

    public string GetSessionValueAsString(HttpContext httpContext, string key)
    {
      return GetSessionValue(httpContext, key)?.ToString();
    }

    //#region IUserStateManager implementation
    //string IUserStateManager.Get(string key)
    //{
    //  return GetSessionValueAsString(key);
    //}

    //void IUserStateManager.Set(string key, string value)
    //{
    //  SetSessionValue(key, value);
    //}
    //#endregion
  }

  ///// <summary>
  ///// Defines properties and methods for user-session-state management.
  ///// </summary>
  //public interface IUserStateManager
  //{
  //  string Get(string key);
  //  void Set(string key, string value);
  //}
}