using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class OfferSentEvent : DomainEvent
    {
        public int OfferId { get; }
        public int ApplicationId { get; }
        public string UserId { get; }
        public int CompanyId { get; }
        public decimal? Salary { get; }
        public DateTime SentAt { get; }

        public OfferSentEvent(int offerId, int applicationId, string userId, int companyId, decimal? salary, DateTime sentAt)
        {
            OfferId = offerId;
            ApplicationId = applicationId;
            UserId = userId;
            CompanyId = companyId;
            Salary = salary;
            SentAt = sentAt;
        }
    }
}

