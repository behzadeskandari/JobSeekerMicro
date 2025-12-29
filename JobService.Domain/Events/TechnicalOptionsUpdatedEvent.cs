using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class TechnicalOptionsUpdatedEvent : DomainEvent
    {
        public int TechnicalOptionId { get; }
        public string Label { get; }
        public DateTime UpdatedAt { get; }

        public TechnicalOptionsUpdatedEvent(int technicalOptionId, string label, DateTime updatedAt)
        {
            TechnicalOptionId = technicalOptionId;
            Label = label;
            UpdatedAt = updatedAt;
        }
    }
}

