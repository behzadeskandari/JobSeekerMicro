using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobSavedEvent : DomainEvent
    {
        public Guid SavedJobId { get; }
        public string UserId { get; }
        public int JobPostId { get; }
        public DateTime SavedAt { get; }

        public JobSavedEvent(Guid savedJobId, string userId, int jobPostId, DateTime savedAt)
        {
            SavedJobId = savedJobId;
            UserId = userId;
            JobPostId = jobPostId;
            SavedAt = savedAt;
        }
    }
}

