using EMX.JobyJobs.DAL.Models;
using EMX.JobyJobs.Shared.ServiceObjects;

namespace EMX.JobyJobs.BL.ServiceObjects
{
    public static class ServiceObjectsExtensions
    {
        public static Worker ToSvc(this worker item)
        {
            return new Worker(item);
        }
        public static worker ToDB(this Worker item)
        {
            var worker = new worker();
            worker.worker_id = item.WorkerId;
            worker.identity_user_id = item.Identity_UserID;
            worker.first_name = item.FirstName;
            worker.last_name = item.LastName;
            worker.phone_number = item.PhoneNumber;
            worker.id_number = item.IdNumber;
            worker.email = item.Email;
            worker.company_id = item.CompanyId;

            return worker;
        }
        public static Company ToSvc(this company item)
        {
            return new Company(item);
        }
        public static Position ToSvc(this position item)
        {
            return new Position(item);
        }
        public static Field ToSvc(this field item)
        {
            return new Field(item);
        }
        public static Application ToSvc(this application item)
        {
            return new Application(item);
        }

        public static conversation_messages ToDB(this ChatMessage item)
        {
            return ToDB(item, false);
        }

        public static conversation_messages ToDB(this ChatMessage item, bool isNew)
        {
            conversation_messages dbObj = new conversation_messages()
            {
                message_id = !isNew ? item.MessageId : 0,
                message_uid = item.MessageUId.ToString(),
                message_type = (int)item.MessageType,
                worker_id = item.WorkerId,
                company_id = item.CompanyId,
                company_person_id = item.CompanyPersonId,
                header = item.Header,
                content = item.Content,
                is_read = item.IsRead,
                date = item.Date,
            };
            return dbObj;
        }

        public static interview ToDB(this Interview item)
        {
            return null;
            //return new interview(item);
        }

        public static Interview ToSvc(this interview item)
        {
            return new Interview(item);
        }
    }
}
