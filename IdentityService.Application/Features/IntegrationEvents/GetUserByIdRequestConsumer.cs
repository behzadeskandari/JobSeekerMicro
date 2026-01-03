using System.Linq;
using FluentResults;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using JobSeeker.Shared.EventBusRabbitMQ;

namespace IdentityService.Application.Features.IntegrationEvents
{
    public class GetUserByIdRequestConsumer : IIntegrationEventHandler<GetUserByIdRequestIntegrationEvent>
    {
        private readonly IIdentityUnitOfWOrk _unitOfWork;

        public GetUserByIdRequestConsumer(IIdentityUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleAsync(GetUserByIdRequestIntegrationEvent @event)
        {
            // Note: This is a request-response pattern that was previously handled by MassTransit.
            // With the new event bus, request-response needs to be handled differently.
            // For now, we'll just log the request. You may need to implement a different pattern.

            var result = await GetUserByIdAsync(@event.UserId);

            // Log the result - in a real implementation, you might publish a response event back
            if (result.IsSuccess)
            {
                Console.WriteLine($"User found: {result.Value.Email}");
            }
            else
            {
                Console.WriteLine($"User not found: {result.Errors.FirstOrDefault()?.Message}");
            }
        }

        private async Task<Result<UserDto>> GetUserByIdAsync(string userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            
            if (user == null)
            {
                return Result.Fail("User not found");
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                PictureUrl = user.PictureUrl,
                IsActive = user.IsActive,
                DateCreated = user.DateCreated
            };

            return Result.Ok(userDto);
        }
    }
}

