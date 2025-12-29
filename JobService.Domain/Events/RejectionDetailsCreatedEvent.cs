using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class RejectionDetailsCreatedEvent : DomainEvent
    {
        public int RejectionId { get; }
        public int ApplicationId { get; }
        public string RejectedById { get; }
        public string Reason { get; }
        public DateTime CreatedAt { get; }

        public RejectionDetailsCreatedEvent(int rejectionId, int applicationId, string rejectedById, string reason, DateTime createdAt)
        {
            RejectionId = rejectionId;
            ApplicationId = applicationId;
            RejectedById = rejectedById;
            Reason = reason;
            CreatedAt = createdAt;
        }
    }
}

