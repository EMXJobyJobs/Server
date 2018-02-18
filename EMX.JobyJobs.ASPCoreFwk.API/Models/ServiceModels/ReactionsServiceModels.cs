using System.ComponentModel.DataAnnotations;

namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{
  public class SeekerLikesPositionServiceRequest
  {
    [Required]
    public int SeekerId { get; set; }
    [Required]
    public int PositionId { get; set; }
  }
  public class EmployerLikesSeekerAndPositionServiceRequest
  {
    [Required]
    public int EmployerPersonId { get; set; }
    [Required]
    public int SeekerId { get; set; }
    [Required]
    public int PositionId { get; set; }
  }

  public class GetLikingCandidatesServiceRequest
  {
    [Required]
    public int EmployerPersonId { get; set; }
    [Required]
    public int PositionId { get; set; }
  }
  public class GetEmployerPersonMatchesServiceRequest
  {
    [Required]
    public int EmployerPersonId { get; set; }
    [Required]
    public int PositionId { get; set; }
  }
}
