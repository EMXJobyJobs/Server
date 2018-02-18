using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.ASPCoreFwk.API.Models.ServiceModels
{

  public static class ServiceModelsExtensions
  {
    public static Seeker ToBusiness(this RegisterSeekerServiceRequest item)    //for create
    {
      var seeker = AutoMapper.Mapper.Instance.Map<RegisterSeekerServiceRequest, Seeker>(item);
      seeker.PasswordHash = item.Password;

      return seeker;
    }


    public static EmployerPerson ToBusiness(this RegisterEmployerServiceRequest item)   //for create
    {
      var employer = new Employer();
      employer.Name = item.EmployerName;
      employer.Address = item.Address;
      employer.About = item.About;
      employer.WebSiteUrl = item.WebSiteURL;
      employer.EmployerHP = item.EmployerHP;
      employer.PhoneNumber = item.PhoneNumber;
      employer.JoinDate = DateTime.Now;
      employer.EmployerHP = item.EmployerHP;
      employer.ContantPersonName = item.EmployerPersonFirstName + " " + item.EmployerPersonLastName;
      employer.ContactPersonPhoneNumber = item.EmployerPersonPhoneNumber;

      var employerPerson = AutoMapper.Mapper.Instance.Map<RegisterEmployerServiceRequest, EmployerPerson>(item);
      employerPerson.Employer = employer;
      employerPerson.FirstName = item.EmployerPersonFirstName;
      employerPerson.LastName = item.EmployerPersonLastName;
      employerPerson.IdNumber = item.EmployerPersonIdNumber;
      employerPerson.RegisterDate = DateTime.Now;
      employerPerson.IsInitiator = true; //sets initiator as true.
      employerPerson.PasswordHash = item.Password;
      employerPerson.JobFunction = item.EmployerPersonJobFunction;

      return employerPerson;
    }

    public static EmployerPerson ToBusiness(this RegisterEmployerPersonServiceRequest item)
    {
      var employerPerson = AutoMapper.Mapper.Instance.Map<RegisterEmployerPersonServiceRequest, EmployerPerson>(item);
      employerPerson.EmployeryId = item.EmployeryId.Value;
      employerPerson.FirstName = item.EmployerPersonFirstName;
      employerPerson.LastName = item.EmployerPersonLastName;
      employerPerson.IdNumber = item.EmployerPersonIdNumber;
      employerPerson.PhoneNumber = item.EmployerPersonPhoneNumber;
      employerPerson.RegisterDate = DateTime.Now;
      employerPerson.IsInitiator = false; //sets initiator as false.
      employerPerson.PasswordHash = item.Password;
      employerPerson.JobFunction = item.EmployerPersonJobFunction;

      return employerPerson;
    }

    public static Position ToBusiness(this PostPositionServiceRequest item)
    {
      var position = AutoMapper.Mapper.Instance.Map<PostPositionServiceRequest, Position>(item);
      position.PositionUID = Guid.Empty;
      position.PositionType = (Enums.PositionTypes)item.PositionType;
      position.Status = Enums.PositionStatuses.Unspecified;
      position.Active = true;

      return position;
    }
    public static Interview ToBusiness(this SetInterviewServiceRequest item)
    {
      var interview = AutoMapper.Mapper.Instance.Map<Interview>(item);
      interview.InterviewUID = Guid.Empty;
      interview.Active = true;

      return interview;
    }

    public static Seeker ToBusiness(this UpdateSeekerServiceRequest item)
    {
      var seeker = AutoMapper.Mapper.Instance.Map<Seeker>(item);
      //seeker.PasswordHash = item.Password;
      //seeker.RegisterDate = DateTime.Now;

      return seeker;
    }

    public static Employer ToBusiness(this UpdateEmployerServiceRequest item)
    {
      var employer = AutoMapper.Mapper.Instance.Map<Employer>(item);
      //seeker.PasswordHash = item.Password;
      //seeker.RegisterDate = DateTime.Now;

      return employer;
    }
    public static EmployerPerson ToBusiness(this UpdateEmployerPersonServiceRequest item)
    {
      var employerPerson = AutoMapper.Mapper.Instance.Map<EmployerPerson>(item);
      //seeker.PasswordHash = item.Password;
      //seeker.RegisterDate = DateTime.Now;

      return employerPerson;
    }

    public static ChatMessage ToBusiness(this SendMessageEmployerToSeekerServiceRequest item)   //for create
    {
      var message = Mapper.Map<ChatMessage>(item);
      message.MessageType = Enums.ChatMessageType.EmployerToSeeker;   //const.

      return message;
    }

    public static ChatMessage ToBusiness(this SendMessageSeekerToEmployerServiceRequest item)   //for create
    {
      var message = Mapper.Map<ChatMessage>(item);
      message.MessageType = Enums.ChatMessageType.SeekerToEmployer;   //const.

      return message;
    }

  }
}
