using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.BusinessObjects;
using EMX.JobyJobs.BL.Interfaces;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.ProxyServices.Managers;
using EMX.JobyJobs.Shared.Definitions;
using EMX.JobyJobs.Shared.Exceptions;
using EMX.JobyJobs.Shared.Helpers;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.BL.Business
{

  public interface IConversationsBL : IBusiness
  {
    void SendMessage(ChatMessage message);
    ConversationMessagesSeekerOverview GetAllConversationsSeeker(int seekerId);
    List<ChatMessage> GetConversationMessagesSeeker(int seekerId, int employerPersonId);
    ConversationMessagesEmployerOverview GetAllConversationsEmployer(int employerPersonId);
    List<ChatMessage> GetConversationMessagesEmployer(int employerPersonId, int seekerId);
    List<ChatMessage> GetEmployerUnassignedMessages(int employerId);
    void AssignEmployerMessage(int messageId, int employerPersonId);
    void SetMessageAsRead(int messageId);
    void ArchiveMessage(int messageId);
  }


  /// <summary>
  /// Handles all business-logic activities around conversations (chat messages) etc.
  /// </summary>
  public class ConversationsBL : IConversationsBL
  {
    //Seeker Point-Of-View (or both)::: 
    public void SendMessage(ChatMessage message)
    {
      //Save in db
      AddMessage(message);

      //Peer-to-peer push
      ChatManager chatManager = new ChatManager();
      chatManager.PostMessage(message);
    }

    private void AddMessage(ChatMessage message)
    {
      using (var db = new JobyJobsDB2())
      {
        //fix param data::
        message.MessageUId = Guid.NewGuid();
        message.Date = DateTime.Now;
        message.Active = true;

        db.conversation_messages.Add(message.ToDB());
        db.SaveChanges();
      }
    }


    //private Task PostMessageAsync(ChatMessage message)
    //{
    //  return new Task(new Action(delegate ()
    //  {
    //    using (var db = new JobyJobsDB2())
    //    {
    //      db.conversation_messages.Add(message.ToDB());
    //      db.SaveChanges();
    //    }
    //  }));
    //}

    public ConversationMessagesSeekerOverview GetAllConversationsSeeker(int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        int takeMax = 10;
        var res = new ConversationMessagesSeekerOverview(seekerId);

        //assigned.
        var dbOverview1 = db.conversation_messages
          .Where(item => item.is_unassigned != true && item.seeker_id == seekerId)
          .GroupBy(item => item.employer_person_id.Value)
          .ToDictionary(kp => kp.Key, kp => kp.Take(10).ToList());
        dbOverview1.ToList().ForEach(kp =>
          res.Chats.Add(kp.Key,
            new ConversationMessagesSeekerOverviewRecord(0, false, kp.Key,
              kp.Value.ToDictionary(kp0 => kp.Key, kp0 => kp0.ToBusiness()))));

        //unassigned.
        var dbOverview2 = db.conversation_messages
          .Where(item => item.is_unassigned == true && item.seeker_id == seekerId)
          .GroupBy(item => item.employer_id)
          .ToDictionary(kp => kp.Key, kp => kp.Take(10).ToList());
        dbOverview2.ToList().ForEach(kp =>
          res.Chats.Add(kp.Key,
            new ConversationMessagesSeekerOverviewRecord(kp.Key, true, 0,
              kp.Value.ToDictionary(kp0 => kp.Key, kp0 => kp0.ToBusiness()))));

        return res;
      }
    }

    public List<ChatMessage> GetConversationMessagesSeeker(int seekerId, int employerPersonId)
    {
      using (var db = new JobyJobsDB2())
      {
        return db.conversation_messages.Where(item => item.seeker_id == seekerId &&
                                                      item.employer_person_id == employerPersonId)
          .ToSelectedList(BusinessObjectsExtensions.ToBusiness);
      }
    }

    public void SetMessageAsRead(int messageId)
    {
      using (var db = new JobyJobsDB2())
      {
        var dbObj = db.conversation_messages.Single(item => item.message_id == messageId);
        dbObj.is_read = true;

        db.SetAsModifiedAndSave(dbObj);
      }
    }

    public void ArchiveMessage(int messageId)
    {
      using (var db = new JobyJobsDB2())
      {
        var dbObj = db.conversation_messages.Find(messageId); //Todo ori. check. check if works.
        dbObj.is_archived = true;

        db.SetAsModifiedAndSave(dbObj);
      }
    }

    //Employer Point-Of-View::::
    public ConversationMessagesEmployerOverview GetAllConversationsEmployer(int employerPersonId)
    {
      using (var db = new JobyJobsDB2())
      {
        int takeMax = 10;
        var res = new ConversationMessagesEmployerOverview(employerPersonId);

        var dbOverview = db.conversation_messages
          .Where(item => item.is_unassigned != true && item.employer_person_id == employerPersonId)
          .GroupBy(item => item.seeker_id)
          .ToDictionary(kp => kp.Key, kp => kp.Take(10).ToList());
        dbOverview.ToList().ForEach(kp =>
          res.Chats.Add(kp.Key,
            new ConversationMessagesEmployerOverviewRecord(kp.Key,
              kp.Value.ToDictionary(kp0 => kp.Key, kp0 => kp0.ToBusiness()))));

        return res;
      }
    }

    public List<ChatMessage> GetConversationMessagesEmployer(int employerPersonId, int seekerId)
    {
      using (var db = new JobyJobsDB2())
      {
        return db.conversation_messages.Where(item => item.employer_person_id == employerPersonId &&
                                                      item.seeker_id == seekerId)
          .ToSelectedList(BusinessObjectsExtensions.ToBusiness);
      }
    }


    public List<ChatMessage> GetEmployerUnassignedMessages(int employerId)
    {
      using (var db = new JobyJobsDB2())
      {
        var messages = db.conversation_messages.Where(item =>
          item.message_type == (int)Enums.ChatMessageType.SeekerToEmployer
          && item.employer_id == employerId
          && item.is_unassigned == true);
        return messages.ToSelectedList(BusinessObjectsExtensions.ToBusiness);
      }
    }

    public void AssignEmployerMessage(int messageId, int employerPersonId)
    {
      using (var db = new JobyJobsDB2())
      {
        var obj = db.conversation_messages.Single(item => item.is_unassigned == true && item.message_id == messageId);
        obj.is_unassigned = false;
        obj.employer_person_id = employerPersonId;
        db.Entry(obj).State = EntityState.Modified;

        db.SaveChanges();
      }
    }



    //Admin Point-Of-View:::
    /// <summary>
    /// Admin side only: deletes the given message.
    /// Note: logical delete.
    /// </summary>
    public void DeletePosition(int messageId)
    {
      using (var db = new JobyJobsDB2())
      {
        var dbObj = db.conversation_messages.Find(messageId);

        //mark as deleted.
        dbObj.active = false;

        db.SetAsModifiedAndSave(dbObj);
      }
    }

  }


}
