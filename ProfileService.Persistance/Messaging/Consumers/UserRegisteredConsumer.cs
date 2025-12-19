using AutoMapper;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Polly;
using ProfileService.Application.Events;
using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
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
        private readonly IProfileServiceUnitOfWork _context;  // Infrastructure dep

        public UserRegisteredConsumer(IProfileServiceUnitOfWork context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
        {
            // Idempotency check (uses Domain entity)
            var existing = await _context.CandidateRepository.GetQueryable()
                .FirstOrDefaultAsync(p => p.UserId == context.Message.UserId);
            if (existing == null)
            {
                // Create and persist (orchestrates via Application if using MediatR; here direct for simplicity)
                var candidate = new Candidate // From Domain
                {
                    Id = Guid.NewGuid(),
                    UserId = context.Message.UserId,
                    FirstName = context.Message.FirstName,
                    LastName = context.Message.LastName,
                    Email = context.Message.Email,
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


            var resumeExits = await  _context.ResumeRepository.GetQueryable().FirstOrDefaultAsync(p => p.UserId == context.Message.UserId);

            if(resumeExits == null)
            {

                var resume = new Resume// From Domain
                {
                    Id = Guid.NewGuid(),
                    UserId = context.Message.UserId,
                    FullName = context.Message.FirstName + " " + context.Message.LastName,
                    Email = context.Message.Email,
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

            var userSettingsExits = await _context.UserSettingsRepository.GetQueryable().FirstOrDefaultAsync(p => p.UserId == context.Message.UserId);

            if(userSettingsExits == null)
            {
                var userSettings = new UserSetting
                {
                    UserId = context.Message.UserId,
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
                userSettingsExits.DateCreated = DateTime.UtcNow,
                userSettingsExits.DateModified = null,
                userSettingsExits.IsActive = true,
                userSettingsExits.Language = "fa",
                userSettingsExits.EmailNotifications = true,
                userSettingsExits.SmsNotifications = true,
                userSettingsExits.TimeZone = "UTC",
                await _context.UserSettingsRepository.UpdateAsync(userSettingsExits);
            }
            await _context.CommitAsync();

            // Optional: Raise domain event or publish integration event via IPublishEndpoint (injected here)
        }
    }
}
