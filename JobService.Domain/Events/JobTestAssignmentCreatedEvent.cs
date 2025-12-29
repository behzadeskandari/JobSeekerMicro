using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobTestAssignmentCreatedEvent : DomainEvent
    {
        public int AssignmentId { get; }
        public int? JobId { get; }
        public bool IsRequired { get; }
        public DateTime CreatedAt { get; }

        public JobTestAssignmentCreatedEvent(int assignmentId, int? jobId, bool isRequired, DateTime createdAt)
        {
            AssignmentId = assignmentId;
            JobId = jobId;
            IsRequired = isRequired;
            CreatedAt = createdAt;
        }
    }
}

