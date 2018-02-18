using System;
using AutoMapper;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.BL.BusinessObjects
{
  public static class BusinessObjectsExtensions
  {



    /// Note: uses 'user' object
    /// Note: uses 'resumes' object
    /// Note: uses 'job-interests' object
    public static Seeker ToBusiness(this seeker item)
    {
      return AutoMapper.Mapper.Instance.Map<seeker, Seeker>(item).CompleteCreate(item);
    }

    /// <summary>
    /// Note: not populates internal 'user' property; 
    /// Note: not populates internal 'seeker_resumes' property; 
    /// Note: not populates internal 'seeker_job_interests' property; 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static seeker ToDB(this Seeker item)
    {
      var seeker = AutoMapper.Mapper.Instance.Map<Seeker, seeker>(item);
      seeker.identity_user_id = item.Identity_UserID;
      seeker.gender = (int)item.Gender;
      seeker.id_number = item.IDNumber;
      seeker.work_state = (int)item.WorkState;
      seeker.lang_id = item.Language != Enums.AppLanguages.Unspecified ? (int?)item.Language : null;

      return seeker;
    }
    public static Employer ToBusiness(this employer item)   //for read
    {
      return AutoMapper.Mapper.Instance.Map<employer, Employer>(item).CompleteCreate(item);
    }
    public static employer ToDB(this Employer item)   //for create
    {
      var employer = AutoMapper.Mapper.Instance.Map<Employer, employer>(item);
      employer.employer_hp = item.EmployerHP;

      return employer;
    }

    public static employer DBUpdateFrom(this employer item, Employer svcItem) //for update
    {
      item.name = svcItem.Name;
      item.address = svcItem.Address;
      item.phone_number = svcItem.PhoneNumber;
      item.contact_person_name = svcItem.ContantPersonName;
      item.contact_person_phone_number = svcItem.ContactPersonPhoneNumber;
      item.employer_hp = svcItem.EmployerHP;
      item.about = svcItem.About;
      item.website_url = svcItem.WebSiteUrl;
      item.logo = svcItem.Logo;
      return item;
    }

    public static EmployerPerson ToBusiness(this employer_persons item)   //for read.
    {
      return AutoMapper.Mapper.Instance.Map<employer_persons, EmployerPerson>(item).CompleteCreate(item);
    }
    public static employer_persons ToDB(this EmployerPerson item)   //for create.
    {
      var employerPerson = AutoMapper.Mapper.Instance.Map<EmployerPerson, employer_persons>(item);
      employerPerson.identity_user_id = item.Identity_UserID;

      return employerPerson;
    }
    public static EmployerPersonInvite ToBusiness(this employer_persons_invites item)   //for read.
    {
      return Mapper.Map<EmployerPersonInvite>(item).CompleteCreate(item);
    }
    public static employer_persons DBUpdateFrom(this employer_persons item, EmployerPerson svcItem)      //for update
    {
      item.first_name = svcItem.FirstName;
      item.last_name = svcItem.LastName;
      item.phone_number = svcItem.PhoneNumber;
      item.id_number = svcItem.IdNumber;
      item.job_function = svcItem.JobFunction;
      return item;
    }
    public static Position ToBusiness(this position item)   //for read
    {
      return AutoMapper.Mapper.Instance.Map<position, Position>(item).CompleteCreate(item);
    }
    public static position ToDB(this Position item)   //for create
    {
      var position = AutoMapper.Mapper.Instance.Map<Position, position>(item);
      position.position_uid = item.PositionUID.ToString();
      position.subprofession_id = item.SubprofessionId;
      position.position_type = (int)item.PositionType;
      position.status_id = (int)item.Status;

      return position;
    }

    public static position DBUpdateFrom(this position item, Position svcItem)    //for update (user side)
    {
      item.position_type = (int)svcItem.PositionType;
      item.title = svcItem.Title;
      item.profession_id = svcItem.ProfessionId;
      item.subprofession_id = svcItem.SubprofessionId;    //not used.
      item.salary_min = svcItem.SalaryMin;
      item.salary_max = svcItem.SalaryMax;
      item.location = svcItem.Location;
      item.description = svcItem.Description;
      //item.status_id = (int)svcItem.Status;    //Note: change state via change-state methods.
     
      return item;
    }

    public static position DBAdminUpdateFrom(this position item, Position svcItem)    //for update (admin side)
    {
      item.position_uid = svcItem.PositionUID.ToString();
      item.employer_id = svcItem.EmployerId;
      item.position_type = (int)svcItem.PositionType;
      item.title = svcItem.Title;
      item.profession_id = svcItem.ProfessionId;
      item.subprofession_id = svcItem.SubprofessionId;    //not used.
      item.salary_min = svcItem.SalaryMin;
      item.salary_max = svcItem.SalaryMax;
      item.location = svcItem.Location;
      item.description = svcItem.Description;
      item.status_id = (int)svcItem.Status;

      return item;
    }

    public static Field ToBusiness(this field item)
    {
      return AutoMapper.Mapper.Instance.Map<field, Field>(item).CompleteCreate(item);
    }
    public static profession ToDB(this Profession item)
    {
      var profession = AutoMapper.Mapper.Instance.Map<Profession, profession>(item);
      profession.profession_id = item.ProfessionId;
      profession.field_id = item.FieldId;
      profession.title = item.Title;
      profession.active = item.Active;

      return profession;
    }
    public static Profession ToBusiness(this profession item)
    {
      return AutoMapper.Mapper.Instance.Map<profession, Profession>(item).CompleteCreate(item);
    }
    public static Application ToBusiness(this application item)
    {
      return AutoMapper.Mapper.Instance.Map<application, Application>(item).CompleteCreate(item);
    }

    public static application ToDB(this Application item)
    {
      var app = AutoMapper.Mapper.Instance.Map<Application, application>(item);
      app.status_id = (int)item.Status;

      return app;
    }

    public static conversation_messages ToDB(this ChatMessage item)
    {
      var dbObj = AutoMapper.Mapper.Instance.Map<ChatMessage, conversation_messages>(item);
      dbObj.message_uid = item.MessageUId.ToString();
      dbObj.message_type = (int)item.MessageType;
      dbObj.employer_person_id = item.EmployerPersonId != 0 ? (int?)item.EmployerPersonId : null;
      return dbObj;
    }

    public static ChatMessage ToBusiness(this conversation_messages item)
    {
      var svcObj = Mapper.Map<ChatMessage>(item);
      svcObj.MessageUId = new Guid(item.message_uid);
      svcObj.MessageType = (Enums.ChatMessageType)item.message_type;
      svcObj.EmployerPersonId = item.employer_person_id ?? 0;
      return svcObj;
    }
    public static Interview ToBusiness(this interview item)
    {
      return AutoMapper.Mapper.Instance.Map<interview, Interview>(item).CompleteCreate(item);
    }
    public static interview ToDB(this Interview item)
    {
      var dbObj = AutoMapper.Mapper.Instance.Map<Interview, interview>(item);
      dbObj.invite_status_id = (int)item.InviteStatus;
      dbObj.invite_cancelReject_reason = item.InviteCancelRejectReason;
      dbObj.application_id = item.ApplicationId;

      return dbObj;
    }

  }
}
