using AutoMapper;
using JobSeeker.Shared.EventBusRabbitMQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using JobSeeker.Shared.Contracts.Integration; // Changed to shared integration event
using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
using ProfileService.Persistance.DbContexts;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Middleware;
namespace ProfileService.Persistance.Messaging.Consumers
{
    public class UserRegisteredConsumer : IConsumer<UserRegisteredIntegrationEvent>
    {
        private readonly IProfileServiceUnitOfWork _context;  // Infrastructure dep

        public UserRegisteredConsumer(IProfileServiceUnitOfWork context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            // Event type verification - now correctly receiving shared integration event
            var @event = context.Message;
            Serilog.Log.Information("ProfileService Consumer: Received UserRegisteredIntegrationEvent for UserId {UserId}", @event.UserId);
            // Idempotency check (uses Domain entity)
            var existing = await _context.CandidateRepository.GetQueryable()
                .FirstOrDefaultAsync(p => p.UserId == @event.UserId);
            if (existing == null)
            {
                // Create and persist (orchestrates via Application if using MediatR; here direct for simplicity)
                var candidate = new Candidate // From Domain
                {
                    Id = Guid.NewGuid(),
                    UserId = @event.UserId,
                    FirstName = @event.FirstName,
                    LastName = @event.LastName,
                    Email = @event.Email,
                };
                await _context.CandidateRepository.AddAsync(candidate);

            }
            else
            {
                existing.FirstName = existing.FirstName;
                existing.LastName = existing.LastName;
                existing.Email = existing.Email;
                existing.UserId = existing.UserId;

                await _context.CandidateRepository.UpdateAsync(existing);
            }


            var resumeExits = await _context.ResumeRepository.GetQueryable().FirstOrDefaultAsync(p => p.UserId == @event.UserId);

            if (resumeExits == null)
            {

                var resume = new Resume// From Domain
                {
                    Id = Guid.NewGuid(),
                    UserId = @event.UserId,
                    FullName = @event.FirstName + " " + @event.LastName,
                    Email = @event.Email,
                };
                await _context.ResumeRepository.AddAsync(resume);

            }
            else
            {
                resumeExits.FullName = resumeExits.FullName;
                resumeExits.Email = resumeExits.Email;
                resumeExits.UserId = resumeExits.UserId;

                await _context.ResumeRepository.UpdateAsync(resumeExits);
            }

            var userSettingsExits = await _context.UserSettingsRepository.GetQueryable().FirstOrDefaultAsync(p => p.UserId == @event.UserId);

            if (userSettingsExits == null)
            {
                var userSettings = new UserSetting
                {
                    UserId = @event.UserId,
                    DateCreated = DateTime.UtcNow,
                    DateModified = null,
                    Id = Guid.NewGuid(),
                    IsActive = true,
                    Language = "fa",
                    EmailNotifications = true,
                    SmsNotifications = true,
                    TimeZone = "UTC",
                };
                await _context.UserSettingsRepository.AddAsync(userSettings);
            }
            else
            {
                userSettingsExits.DateCreated = DateTime.UtcNow;
                userSettingsExits.DateModified = null;
                userSettingsExits.IsActive = true;
                userSettingsExits.Language = "fa";
                userSettingsExits.EmailNotifications = true;
                userSettingsExits.SmsNotifications = true;
                userSettingsExits.TimeZone = "UTC";
                await _context.UserSettingsRepository.UpdateAsync(userSettingsExits);
            }
            await _context.CommitAsync();

        }
    }
}
