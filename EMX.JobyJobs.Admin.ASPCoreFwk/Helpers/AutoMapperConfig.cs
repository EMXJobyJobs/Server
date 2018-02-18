using AutoMapper;
using EMX.JobyJobs.Admin.ASPCoreFwk.Models;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.Admin.ASPCoreFwk.Helpers
{

  /// <summary>
  ///data objects - business objects
  /// </summary>
  public class AutoMapperProfile1 : Profile
  {
    public AutoMapperProfile1() :
      base("DBtoSVC", Configure)
    {
      //Add as many of these lines as you need to map your objects
      CreateMap<seeker, Seeker>();
      CreateMap<employer, Employer>();
      CreateMap<employer_persons, EmployerPerson>();
      CreateMap<employer_persons_invites, EmployerPersonInvite>();
      CreateMap<position, Position>();
      CreateMap<application, Application>();
      CreateMap<interview, Interview>();
      CreateMap<field, Field>();
      CreateMap<profession, Profession>();
      CreateMap<conversation_messages, ChatMessage>();
    }

    private static void Configure(IProfileExpression cfg)
    {
      cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
      cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
    }
  }




  /// <summary>
  ///business objects - data objects
  /// </summary>
  public class AutoMapperProfile2 : Profile
  {
    public AutoMapperProfile2()
      : base("SVCtoDB", Configure)
    {
      //Add as many of these lines as you need to map your objects
      CreateMap<Seeker, seeker>().ForMember(item => item.language, opt => opt.Ignore());
      CreateMap<Employer, employer>();
      CreateMap<EmployerPerson, employer_persons>();
      CreateMap<EmployerPersonInvite, employer_persons_invites>();
      CreateMap<Position, position>();
      CreateMap<Application, application>();
      CreateMap<Interview, interview>();
      CreateMap<Field, field>();
      CreateMap<Profession, profession>();
      CreateMap<ChatMessage, conversation_messages>();
    }

    private static void Configure(IProfileExpression cfg)
    {
      cfg.SourceMemberNamingConvention = new PascalCaseNamingConvention();
      cfg.DestinationMemberNamingConvention = new LowerUnderscoreNamingConvention();
    }
  }





  /// <summary>
  /// business objects - service models
  /// </summary>
  public class AutoMapperProfile3 : Profile
  {
    public AutoMapperProfile3()
      : base("SvcToView", Configure)
    {
      //Add as many of these lines as you need to map your objects
    }

    private static void Configure(IProfileExpression cfg)
    {
      cfg.SourceMemberNamingConvention = new PascalCaseNamingConvention();
      cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
    }
  }





  /// <summary>
  ///S models - business objects
  /// </summary>
  public class AutoMapperProfile4 : Profile
  {
    public AutoMapperProfile4()
      : base("ViewtoSvc", Configure)
    {
      //Add as many of these lines as you need to map your objects
      CreateMap<CreateUpdatePositionViewModel, Position>();
    }

    private static void Configure(IProfileExpression cfg)
    {
      cfg.SourceMemberNamingConvention = new PascalCaseNamingConvention();
      cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
    }
  }
}