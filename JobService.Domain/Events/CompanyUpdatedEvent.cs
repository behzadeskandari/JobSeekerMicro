using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class CompanyUpdatedEvent : DomainEvent
    {
        public int CompanyId { get; }
        public string Name { get; }
        public DateTime UpdatedAt { get; }

        public CompanyUpdatedEvent(int companyId, string name, DateTime updatedAt)
        {
            CompanyId = companyId;
            Name = name;
            UpdatedAt = updatedAt;
        }
    }
}

