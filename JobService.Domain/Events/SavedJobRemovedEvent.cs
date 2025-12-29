using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class SavedJobRemovedEvent : DomainEvent
    {
        public int SavedJobId { get; }
        public string UserId { get; }
        public int JobPostId { get; }
        public DateTime RemovedAt { get; }

        public SavedJobRemovedEvent(int savedJobId, string userId, int jobPostId, DateTime removedAt)
        {
            SavedJobId = savedJobId;
            UserId = userId;
            JobPostId = jobPostId;
            RemovedAt = removedAt;
        }
    }
}

