using JobSeeker.Shared.Models;

namespace JobSeeker.Shared.PushNotifications.Interfaces
{
    public interface IPushSubscriptionRepository
    {
        Task<List<PushSubscription>> GetAllAsync();
        Task<List<PushSubscription>> GetByUserIdAsync(string userId);
        Task<PushSubscription?> GetByEndpointAsync(string endpoint);
        Task AddOrUpdateAsync(PushSubscription subscription);
        Task RemoveAsync(PushSubscription subscription);
    }
}

