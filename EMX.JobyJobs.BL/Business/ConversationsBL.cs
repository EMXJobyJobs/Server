using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMX.JobyJobs.BL.ServiceObjects;
using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.BL.Business
{
    /// <summary>
    /// Handles all business-logic activities around conversations (chat messages) etc.
    /// </summary>
    public static class ConversationsBL
    {
        public static Task AddMessageAsync(ChatMessage message)
        {
            return new Task(new Action(delegate()
            {
                using (var db = new JobyJobsDB2())
                {
                    db.conversation_messages.Add(message.ToDB(true));
                }
            }));
        }
    }
}
