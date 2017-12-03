using System;

namespace EMX.JobyJobs.Shared.ServiceObjects
{
    public enum ChatMessageType
    {
        Unspecified = 0,
        WorkerToCompany = 1,
        CompanyToWorker = 2,
    }
    public class ChatMessage
    {
        public int MessageId { get; set; }
        public Guid MessageUId { get; set; }
        public ChatMessageType MessageType { get; set; }
        public int WorkerId { get; set; }
        public int CompanyId { get; set; }
        public int CompanyPersonId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}
