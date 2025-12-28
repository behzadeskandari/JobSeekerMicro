namespace JobSeeker.Shared.Contracts.Integration
{
    public class UserRegisteredIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public UserRegisteredIntegrationEvent()
        {
        }

        public UserRegisteredIntegrationEvent(string userId, string email, string role, string? firstName, string? lastName)
        {
            UserId = userId;
            Email = email;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}

