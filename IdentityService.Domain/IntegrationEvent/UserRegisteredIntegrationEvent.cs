
using JobSeeker.Shared.Contracts.Integration;

namespace IdentityService.Domain.IntegrationEventSourcing
{
    public sealed class UserRegisteredIntegrationEvent : IntegrationEvent  
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Role { get; }
        public string? FirstName { get; }
        public string? LastName { get; }

        public UserRegisteredIntegrationEvent(Guid userId, string email, string role, string? firstName, string? lastName)
        {
            UserId = userId;
            Email = email;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
