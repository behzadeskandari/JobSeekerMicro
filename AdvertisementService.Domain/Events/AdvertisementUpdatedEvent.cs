using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Events
{
    public class AdvertisementUpdatedEvent : DomainEvent
    {
        public Guid AdvertisementId { get; }
        public string Title { get; }
        public bool IsApproved { get; }
        public bool IsPaid { get; }

        public AdvertisementUpdatedEvent(Guid advertisementId, string title, bool isApproved, bool isPaid)
        {
            AdvertisementId = advertisementId;
            Title = title;
            IsApproved = isApproved;
            IsPaid = isPaid;
        }
    }
}

