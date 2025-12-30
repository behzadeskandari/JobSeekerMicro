using JobSeeker.Shared.Models;

namespace JobSeeker.Shared.PushNotifications.Interfaces
{
    public interface IPushSubscriptionRepository
    {
        Task<List<AppPushSubscriptions>> GetAllAsync();
        Task<List<AppPushSubscriptions>> GetByUserIdAsync(string userId);
        Task<AppPushSubscriptions?> GetByEndpointAsync(string endpoint);
        Task AddOrUpdateAsync(AppPushSubscriptions subscription);
        Task RemoveAsync(AppPushSubscriptions subscription);
    }
}

