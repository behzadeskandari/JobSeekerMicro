
using JobSeeker.Shared.Contracts.Integration;

namespace IdentityService.Domain.IntegrationEventSourcing
{
    public sealed class UserRegisteredEvent : IntegrationEvent  
    {
        public string UserId { get; }
        public string Email { get; }
        public string Role { get; }
        public string? FirstName { get; }
        public string? LastName { get; }

        public UserRegisteredEvent(string userId, string email, string role, string? firstName, string? lastName)
        {
            UserId = userId;
            Email = email;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
