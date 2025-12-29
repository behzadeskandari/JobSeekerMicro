using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class ApplicationRejectedEvent : DomainEvent
    {
        public int ApplicationId { get; }
        public string UserId { get; }
        public string? Reason { get; }
        public DateTime RejectedAt { get; }

        public ApplicationRejectedEvent(int applicationId, string userId, string? reason, DateTime rejectedAt)
        {
            ApplicationId = applicationId;
            UserId = userId;
            Reason = reason;
            RejectedAt = rejectedAt;
        }
    }
}

