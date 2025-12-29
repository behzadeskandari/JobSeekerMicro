using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobUnsavedEvent : DomainEvent
    {
        public int SavedJobId { get; }
        public string UserId { get; }
        public int JobPostId { get; }
        public DateTime UnsavedAt { get; }

        public JobUnsavedEvent(int savedJobId, string userId, int jobPostId, DateTime unsavedAt)
        {
            SavedJobId = savedJobId;
            UserId = userId;
            JobPostId = jobPostId;
            UnsavedAt = unsavedAt;
        }
    }
}

