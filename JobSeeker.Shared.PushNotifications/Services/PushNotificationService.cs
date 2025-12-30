using System.Text;
using System.Text.Json;
using JobSeeker.Shared.Models;
using JobSeeker.Shared.PushNotifications.DTOs;
using JobSeeker.Shared.PushNotifications.Interfaces;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JobSeeker.Shared.PushNotifications.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly IPushSubscriptionRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PushNotificationService> _logger;
        private readonly PushServiceClient _pushClient;

        public PushNotificationService(
            IPushSubscriptionRepository repository,
            IConfiguration configuration,
            ILogger<PushNotificationService> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;

            var publicKey = _configuration["PushNotifications:Vapid:PublicKey"] 
                ?? throw new InvalidOperationException("PushNotifications:Vapid:PublicKey is required");
            var privateKey = _configuration["PushNotifications:Vapid:PrivateKey"] 
                ?? throw new InvalidOperationException("PushNotifications:Vapid:PrivateKey is required");
            var subject = _configuration["PushNotifications:Vapid:Subject"] ?? "mailto:admin@jobseeker.com";

            _pushClient = new PushServiceClient
            {
                DefaultAuthentication = new VapidAuthentication(publicKey, privateKey)
            };
        }

        public async Task SendToAllAsync(string title, string body, string? icon = null, string? url = null)
        {
            var subscriptions = await _repository.GetAllAsync();
            await SendToSubscriptionsAsync(subscriptions, title, body, icon, url);
        }

        public async Task SendToUserAsync(string userId, string title, string body, string? icon = null, string? url = null)
        {
            var subscriptions = await _repository.GetByUserIdAsync(userId);
            await SendToSubscriptionsAsync(subscriptions, title, body, icon, url);
        }

        private async Task SendToSubscriptionsAsync(List<AppPushSubscriptions> subscriptions, string title, string body, string? icon, string? url)
        {
            var payload = JsonSerializer.Serialize(new { title, body, icon, url });

            foreach (var subscription in subscriptions)
            {
                try
                {
                    var pushSubscription = new PushSubscription();

                    pushSubscription.Endpoint = subscription.Endpoint;
                    pushSubscription.Keys.Add("P256DH", subscription.P256DH);
                    pushSubscription.Keys.Add("Auth", subscription.Auth);
                    pushSubscription.Keys.Add("ExpirationTime", subscription.ExpirationTime.ToString());

                    var message = new PushMessage(payload)
                    {
                        TimeToLive = 3000,
                        Content = "message",
                    };

                    await _pushClient.RequestPushMessageDeliveryAsync(pushSubscription, message);
                }
                catch (PushServiceClientException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Gone || ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _logger.LogWarning(ex, "Push subscription expired or not found. Removing subscription: {Endpoint}", subscription.Endpoint);
                    await RemoveSubscriptionAsync(subscription.Endpoint);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to send push notification to subscription: {Endpoint}", subscription.Endpoint);
                    // Continue with other subscriptions
                }
            }
        }

        public async Task SaveSubscriptionAsync(string userId, PushSubscriptionDto dto)
        {
            var existing = await _repository.GetByEndpointAsync(dto.Endpoint);

            if (existing != null)
            {
                // Update existing subscription
                existing.UserId = userId;
                existing.P256DH = dto.P256DH;
                existing.Auth = dto.Auth;
                existing.ExpirationTime = dto.ExpirationTime;
                await _repository.AddOrUpdateAsync(existing);
            }
            else
            {
                // Create new subscription
                var subscription = new AppPushSubscriptions
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Endpoint = dto.Endpoint,
                    P256DH = dto.P256DH,
                    Auth = dto.Auth,
                    ExpirationTime = dto.ExpirationTime,
                    DateCreated = DateTime.UtcNow
                };
                await _repository.AddOrUpdateAsync(subscription);
            }
        }

        public async Task RemoveSubscriptionAsync(string endpoint)
        {
            var subscription = await _repository.GetByEndpointAsync(endpoint);
            if (subscription != null)
            {
                await _repository.RemoveAsync(subscription);
            }
        }
    }
}

