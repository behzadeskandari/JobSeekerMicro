using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Domain.IntegrationEvent
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
