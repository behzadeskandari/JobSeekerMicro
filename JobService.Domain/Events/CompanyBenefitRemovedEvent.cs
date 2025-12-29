using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class CompanyBenefitRemovedEvent : DomainEvent
    {
        public int BenefitId { get; }
        public int CompanyId { get; }
        public DateTime RemovedAt { get; }

        public CompanyBenefitRemovedEvent(int benefitId, int companyId, DateTime removedAt)
        {
            BenefitId = benefitId;
            CompanyId = companyId;
            RemovedAt = removedAt;
        }
    }
}

