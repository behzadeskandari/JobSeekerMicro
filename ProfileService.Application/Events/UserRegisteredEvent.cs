using JobSeeker.Shared.Contracts.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Application.Events
{
    public class UserRegisteredEvent : IntegrationEvent
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
