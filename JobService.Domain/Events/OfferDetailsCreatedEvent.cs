using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class OfferDetailsCreatedEvent : DomainEvent
    {
        public int OfferId { get; }
        public int ApplicationId { get; }
        public string OfferedById { get; }
        public int CompanyId { get; }
        public decimal Salary { get; }
        public DateTime CreatedAt { get; }

        public OfferDetailsCreatedEvent(int offerId, int applicationId, string offeredById, int companyId, decimal salary, DateTime createdAt)
        {
            OfferId = offerId;
            ApplicationId = applicationId;
            OfferedById = offeredById;
            CompanyId = companyId;
            Salary = salary;
            CreatedAt = createdAt;
        }
    }
}

