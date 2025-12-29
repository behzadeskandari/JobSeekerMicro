using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class CompanyJobPreferencesUpdatedEvent : DomainEvent
    {
        public int PreferenceId { get; }
        public int JobPostId { get; }
        public DateTime UpdatedAt { get; }

        public CompanyJobPreferencesUpdatedEvent(int preferenceId, int jobPostId, DateTime updatedAt)
        {
            PreferenceId = preferenceId;
            JobPostId = jobPostId;
            UpdatedAt = updatedAt;
        }
    }
}

