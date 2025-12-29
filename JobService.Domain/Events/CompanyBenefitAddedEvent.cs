using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class CompanyBenefitAddedEvent : DomainEvent
    {
        public int BenefitId { get; }
        public int CompanyId { get; }
        public string Name { get; }
        public DateTime CreatedAt { get; }

        public CompanyBenefitAddedEvent(int benefitId, int companyId, string name, DateTime createdAt)
        {
            BenefitId = benefitId;
            CompanyId = companyId;
            Name = name;
            CreatedAt = createdAt;
        }
    }
}

