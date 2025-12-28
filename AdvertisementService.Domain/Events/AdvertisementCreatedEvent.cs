using JobSeeker.Shared.Kernel.Domain;

namespace AdvertisementService.Domain.Events
{
    public class AdvertisementCreatedEvent : DomainEvent
    {
        public Guid AdvertisementId { get; }
        public string UserId { get; }
        public int CompanyId { get; }
        public int CategoryId { get; }
        public string Title { get; }
        public bool IsPaid { get; }

        public AdvertisementCreatedEvent(Guid advertisementId, string userId, int companyId, int categoryId, string title, bool isPaid)
        {
            AdvertisementId = advertisementId;
            UserId = userId;
            CompanyId = companyId;
            CategoryId = categoryId;
            Title = title;
            IsPaid = isPaid;
        }
    }
}

