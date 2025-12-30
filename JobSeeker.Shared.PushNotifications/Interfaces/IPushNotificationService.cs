using JobSeeker.Shared.PushNotifications.DTOs;

namespace JobSeeker.Shared.PushNotifications.Interfaces
{
    public interface IPushNotificationService
    {
        Task SendToAllAsync(string title, string body, string? icon = null, string? url = null);
        Task SendToUserAsync(string userId, string title, string body, string? icon = null, string? url = null);
        Task SaveSubscriptionAsync(string userId, PushSubscriptionDto dto);
        Task RemoveSubscriptionAsync(string endpoint);
    }
}

