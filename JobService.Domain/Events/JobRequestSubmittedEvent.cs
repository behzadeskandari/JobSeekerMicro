using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobRequestSubmittedEvent : DomainEvent
    {
        public int RequestId { get; }
        public string UserId { get; }
        public int JobPostId { get; }
        public DateTime SubmittedAt { get; }

        public JobRequestSubmittedEvent(int requestId, string userId, int jobPostId, DateTime submittedAt)
        {
            RequestId = requestId;
            UserId = userId;
            JobPostId = jobPostId;
            SubmittedAt = submittedAt;
        }
    }
}

