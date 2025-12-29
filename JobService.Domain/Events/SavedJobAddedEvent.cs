using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class SavedJobAddedEvent : DomainEvent
    {
        public int SavedJobId { get; }
        public string UserId { get; }
        public int JobPostId { get; }
        public DateTime SavedAt { get; }

        public SavedJobAddedEvent(int savedJobId, string userId, int jobPostId, DateTime savedAt)
        {
            SavedJobId = savedJobId;
            UserId = userId;
            JobPostId = jobPostId;
            SavedAt = savedAt;
        }
    }
}

