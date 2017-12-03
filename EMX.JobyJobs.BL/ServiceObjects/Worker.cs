using System;
using EMX.JobyJobs.DAL.Models;

namespace EMX.JobyJobs.BL.ServiceObjects
{
    /// <summary>
    /// Represents a worker(candidate) service class.
    /// </summary>
    public class Worker
    {
        public int WorkerId { get; set; }
        public int CompanyId { get; set; }
        public string Identity_UserID { get; set; }   //NOT PRELIMINARY;
        public string IdNumber { get; set; }  //PRELIMINARY; MAY NOT CHANGE
        public string FirstName { get; set; }   //PRELIMINARY; MAY CHANGE
        public string LastName { get; set; }   //PRELIMINARY; MAY CHANGE
        public string PhoneNumber { get; set; }  //PRELIMINARY; MAY NOT CHANGE
        public string Email { get; set; }  //PRELIMINARY; MAY NOT CHANGE
        public bool Registered { get; set; }
        public DateTime? RegisterDate { get; set; }
        public Worker()
        {

        }

        public Worker(worker item)
        {
            this.WorkerId = item.worker_id;
            this.CompanyId = item.company_id;
            this.Identity_UserID = item.identity_user_id;
            this.IdNumber = item.id_number;
            this.FirstName = item.first_name;
            this.LastName = item.last_name;
            this.PhoneNumber = item.phone_number;
            this.Email = item.email;
            //this.Registered = item.registered.GetValueOrDefault(false);
            //this.RegisterDate = item.register_date;
        }

        public Worker(int workerId, int companyId, string identityUserId, string idNumber, string firstName, string lastName, string phoneNumber, string email, bool registered, DateTime? registerDate)
        {
            WorkerId = workerId;
            CompanyId = companyId;
            Identity_UserID = identityUserId;
            IdNumber = idNumber;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
            Registered = registered;
            RegisterDate = registerDate;
        }
    }
}
