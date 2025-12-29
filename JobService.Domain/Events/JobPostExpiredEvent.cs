using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobPostExpiredEvent : DomainEvent
    {
        public int JobPostId { get; }
        public string Title { get; }
        public DateTime ExpiredAt { get; }

        public JobPostExpiredEvent(int jobPostId, string title, DateTime expiredAt)
        {
            JobPostId = jobPostId;
            Title = title;
            ExpiredAt = expiredAt;
        }
    }
}

