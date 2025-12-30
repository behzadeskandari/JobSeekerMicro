using JobSeeker.Shared.PushNotifications.Interfaces;

namespace JobSeeker.Shared.PushNotifications.Examples
{
    /// <summary>
    /// Example usage of IPushNotificationService in handlers
    /// </summary>
    public class PushNotificationUsageExample
    {
        private readonly IPushNotificationService _pushNotificationService;

        public PushNotificationUsageExample(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        /// <summary>
        /// Example: Send notification to all users when a new job post is published
        /// </summary>
        public async Task ExampleJobPostPublished(string jobTitle, int jobId)
        {
            await _pushNotificationService.SendToAllAsync(
                title: "آگهی جدید",
                body: $"آگهی {jobTitle} منتشر شد",
                icon: "/icon.png",
                url: $"/job/{jobId}"
            );
        }

        /// <summary>
        /// Example: Send notification to a specific user for login alert
        /// </summary>
        public async Task ExampleLoginAlert(string userId)
        {
            await _pushNotificationService.SendToUserAsync(
                userId: userId,
                title: "ورود موفق",
                body: "شما با موفقیت وارد حساب کاربری خود شدید",
                icon: "/icon.png"
            );
        }

        /// <summary>
        /// Example: Send notification for payment success
        /// </summary>
        public async Task ExamplePaymentSuccess(string userId, decimal amount)
        {
            await _pushNotificationService.SendToUserAsync(
                userId: userId,
                title: "پرداخت موفق",
                body: $"پرداخت مبلغ {amount:N0} تومان با موفقیت انجام شد",
                icon: "/icon.png",
                url: "/payments"
            );
        }
    }
}

