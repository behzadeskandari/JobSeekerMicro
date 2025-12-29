using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobApplicationUpdatedEvent : DomainEvent
    {
        public int JobApplicationId { get; }
        public string Status { get; }
        public DateTime UpdatedAt { get; }

        public JobApplicationUpdatedEvent(int jobApplicationId, string status, DateTime updatedAt)
        {
            JobApplicationId = jobApplicationId;
            Status = status;
            UpdatedAt = updatedAt;
        }
    }
}

