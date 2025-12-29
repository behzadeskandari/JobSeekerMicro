using System.Linq;
using FluentResults;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using JobSeeker.Shared.Contracts.IntegrationEvents;
using MassTransit;

namespace IdentityService.Application.Features.IntegrationEvents
{
    public class GetUserByIdRequestConsumer : IConsumer<GetUserByIdRequestIntegrationEvent>
    {
        private readonly IIdentityUnitOfWOrk _unitOfWork;

        public GetUserByIdRequestConsumer(IIdentityUnitOfWOrk unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Consume(ConsumeContext<GetUserByIdRequestIntegrationEvent> context)
        {
            var request = context.Message;
            var result = await GetUserByIdAsync(request.UserId);

            var response = new GetUserByIdResponseIntegrationEvent(
                requestId: request.RequestId,
                isSuccess: result.IsSuccess,
                errorMessage: result.IsFailed ? result.Errors.FirstOrDefault()?.Message : null,
                user: result.IsSuccess ? result.Value : null
            );

            await context.RespondAsync(response);
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

