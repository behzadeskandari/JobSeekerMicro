using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Common;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Domain.Events;
using AdvertisementService.Persistence.DbContexts;
using JobSeeker.Shared.Contracts.Integration;
using JobSeeker.Shared.Kernel.Domain;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementService.Persistence.DomainEvents
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly AdvertismentDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public DomainEventDispatcher(AdvertismentDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        public async Task DispatchDomainEventsAsync()
        {
            var domainEventEntities = _context.ChangeTracker
                .Entries<EntityBase<Guid>>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
                .ToList();

            var domainEventEntitiesInt = _context.ChangeTracker
                .Entries<EntityBaseInt>()
                .Where(x => x.Entity.DomainEvents.Any())
                .Select(x => x.Entity)
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

            // Clear domain events
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
                AdvertisementCreatedEvent e => new AdvertisementCreatedIntegrationEvent(
                    e.AdvertisementId,
                    e.UserId,
                    e.CompanyId,
                    e.CategoryId,
                    e.Title,
                    e.IsPaid),
                PaymentProcessedEvent e => new PaymentProcessedIntegrationEvent(
                    e.PaymentId,
                    e.AdvertisementId,
                    e.UserId,
                    e.Amount,
                    e.Status),
                OrderPlacedEvent e => new OrderPlacedIntegrationEvent(
                    e.OrderId,
                    e.PricingPlanId,
                    e.UserId,
                    e.TotalAmount),
                ProductInventoryUpdatedEvent e => new ProductInventoryUpdatedIntegrationEvent(
                    e.ProductInventoryId,
                    e.ProductId,
                    e.QuantityOnHand),
                SalesOrderCreatedEvent e => new SalesOrderCreatedIntegrationEvent(
                    e.SalesOrderId,
                    e.CustomerId,
                    e.IsPaid),
                _ => null
            };
        }
    }
}

