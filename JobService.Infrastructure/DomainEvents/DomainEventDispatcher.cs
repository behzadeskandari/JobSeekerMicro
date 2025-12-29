using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using JobService.Application.Interfaces;
using JobService.Domain.Common;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using JobService.Persistence.DbContexts;
using JobSeeker.Shared.Contracts.Integration;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Kernel.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace JobService.Infrastructure.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly JobDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public DomainEventDispatcher(JobDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task DispatchDomainEventsAsync()
        {
            var domainEventEntities = _context.ChangeTracker
                .Entries<AuditableEntityBase<Guid>>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .Where(x => x != null)
                .ToList();

            var domainEventEntitiesInt = _context.ChangeTracker
                .Entries<AuditableEntityBaseInt>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .Where(x => x != null)
                .ToList();


            var domainEvents = domainEventEntities
                 .SelectMany(x => x.DomainEvents)
                 .Concat(domainEventEntitiesInt.SelectMany(x => x.DomainEvents))
                 .ToList();

            foreach (var domainEvent in domainEvents)
            {
                var integrationEvent = MapToIntegrationEvent(domainEvent);
                if (integrationEvent != null)
                {
                    // Save to outbox
                    var outboxMessage = new OutboxMessage
                    {
                        Id = integrationEvent.Id,
                        Type = integrationEvent.GetType().Name,
                        Content = JsonSerializer.Serialize(integrationEvent),
                        OccurredOn = integrationEvent.OccurredOn,
                        Published = false
                    };

                    await _context.OutboxMessage.AddAsync(outboxMessage);

                    // Publish via MassTransit
                    await _publishEndpoint.Publish(integrationEvent);
                }
            }

            foreach (var entity in domainEventEntities)
            {
                entity.ClearDomainEvents();
            }

            foreach (var entity in domainEventEntitiesInt)
            {
                entity.ClearDomainEvents();
            }
        }

        private IntegrationEvent? MapToIntegrationEvent(DomainEvent domainEvent)
        {
            return domainEvent switch
            {
                JobPostPublishedEvent e => new JobPostPublishedIntegrationEvent(
                    e.JobPostId,
                    e.CompanyId,
                    e.Title,
                    e.JobCategoryId,
                    e.ProvinceId,
                    e.CityId,
                    e.UserId,
                    e.PublishedAt),
                JobApplicationSubmittedEvent e => new JobApplicationSubmittedIntegrationEvent(
                    e.JobApplicationId,
                    e.JobPostId,
                    e.UserId,
                    e.ResumeUrl,
                    e.AppliedAt),
                CompanyCreatedEvent e => new JobSeeker.Shared.Contracts.Integration.CompanyCreatedIntegrationEvent(
                    e.CompanyId,
                    e.UserId,
                    e.Name, // CompanyName
                    e.isActive,
                    e.IsVerified,
                    e.LogoUrl),
                OfferSentEvent e => new OfferSentIntegrationEvent(
                    e.OfferId,
                    e.ApplicationId,
                    e.UserId,
                    e.CompanyId,
                    e.Salary),
                InterviewScheduledEvent e => new InterviewScheduledIntegrationEvent(
                    e.InterviewId,
                    e.ApplicationId,
                    e.InterviewDate),
                ApplicationRejectedEvent e => new JobApplicationRejectedIntegrationEvent(
                    e.ApplicationId,
                    e.UserId,
                    e.Reason),
                JobSavedEvent e => new JobSavedIntegrationEvent(
                    e.SavedJobId,
                    e.UserId,
                    e.JobPostId),
                SavedJobAddedEvent e => new JobSavedIntegrationEvent(
                    e.SavedJobId,
                    e.UserId,
                    e.JobPostId),
                CompanyFollowedEvent e => new CompanyFollowedIntegrationEvent(
                    e.FollowId,
                    e.UserId,
                    e.CompanyId,
                    e.FollowedAt),
                _ => null
            };
        }
    }
}

