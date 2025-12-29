using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobUpdatedEvent : DomainEvent
    {
        public int JobId { get; }
        public string Title { get; }
        public int CompanyId { get; }
        public DateTime UpdatedAt { get; }

        public JobUpdatedEvent(int jobId, string title, int companyId, DateTime updatedAt)
        {
            JobId = jobId;
            Title = title;
            CompanyId = companyId;
            UpdatedAt = updatedAt;
        }
    }
}

