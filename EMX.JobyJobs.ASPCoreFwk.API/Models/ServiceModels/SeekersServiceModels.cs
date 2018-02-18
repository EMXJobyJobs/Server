using System;
using EMX.JobyJobs.Shared.Definitions;

namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{
  public class UpdateSeekerServiceRequest
  {
    public int SeekerId { get; set; }   //get from token
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public Enums.Gender Gender { get; set; }
    public string IDNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public Enums.SeekerWorkStates WorkState { get; set; }
  }
  public class UpdateSeekerWorkStateServiceRequest
  {
    public int SeekerId { get; set; }   //get from token
     public Enums.SeekerWorkStates NewState { get; set; }
  }


}
