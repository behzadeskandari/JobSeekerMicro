using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class OfferDetailsUpdatedEvent : DomainEvent
    {
        public int OfferId { get; }
        public int ApplicationId { get; }
        public string Status { get; }
        public DateTime UpdatedAt { get; }

        public OfferDetailsUpdatedEvent(int offerId, int applicationId, string status, DateTime updatedAt)
        {
            OfferId = offerId;
            ApplicationId = applicationId;
            Status = status;
            UpdatedAt = updatedAt;
        }
    }
}

