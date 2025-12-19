using AutoMapper;
using MassTransit;
using ProfileService.Application.Events;
using ProfileService.Persistance.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProfileService.Persistance.Messaging.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegisteredEvent>
    {
        private readonly ProfileServiceDbContext _context;  // Infrastructure dep

        public UserRegisteredConsumer(ProfileServiceDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            // Idempotency check (uses Domain entity)
            //var existing = await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == context.Message.UserId);
            //if (existing != null) return;

            // Create and persist (orchestrates via Application if using MediatR; here direct for simplicity)
            //var profile = new Profile  // From Domain
            //{
            //    Id = Guid.NewGuid(),
            //    UserId = context.Message.UserId,
            //    FirstName = context.Message.FirstName,
            //    LastName = context.Message.LastName,
            //    Bio = context.Message.Bio,
            //    CreatedAt = context.Message.RegisteredAt
            //};

            //_context.Profiles.Add(profile);
            //await _context.SaveChangesAsync();

            // Optional: Raise domain event or publish integration event via IPublishEndpoint (injected here)
        }
    }
}
