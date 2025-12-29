using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobUnsavedEvent : DomainEvent
    {
        public Guid SavedJobId { get; }
        public string UserId { get; }
        public int JobPostId { get; }
        public DateTime UnsavedAt { get; }

        public JobUnsavedEvent(Guid savedJobId, string userId, int jobPostId, DateTime unsavedAt)
        {
            SavedJobId = savedJobId;
            UserId = userId;
            JobPostId = jobPostId;
            UnsavedAt = unsavedAt;
        }
    }
}

