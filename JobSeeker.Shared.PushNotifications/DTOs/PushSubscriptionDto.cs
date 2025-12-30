namespace JobSeeker.Shared.PushNotifications.DTOs
{
    public class PushSubscriptionDto
    {
        public string Endpoint { get; set; } = string.Empty;
        public string P256DH { get; set; } = string.Empty;
        public string Auth { get; set; } = string.Empty;
        public DateTime? ExpirationTime { get; set; }
    }
}

