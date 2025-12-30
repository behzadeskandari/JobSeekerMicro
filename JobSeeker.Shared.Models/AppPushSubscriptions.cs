namespace JobSeeker.Shared.Models
{
    public class AppPushSubscriptions
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserId { get; set; } = string.Empty; // string from Identity
        public string Endpoint { get; set; } = string.Empty;
        public string P256DH { get; set; } = string.Empty;
        public string Auth { get; set; } = string.Empty;
        public DateTime? ExpirationTime { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
