using System;
using System.Threading.Tasks;
using AdvertisementService.Application.Features.Advertisement.Command;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Events;
using FluentResults;
using MediatR;

namespace AdvertisementService.Application.Features.Advertisement.Handlers
{
    public class UpdateAdvertisementHandler : IRequestHandler<UpdateAdvertisementCommand, Result>
    {
        private readonly IAdvertisementUnitOfWork _repository;

        public UpdateAdvertisementHandler(IAdvertisementUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            AdvertisementService.Domain.Entities.Advertisement advertisement = await _repository.AdvertisementRepository.GetByIdAsync(request.Id);
            if (advertisement == null)
            {
                return Result.Fail("Advertisement not found");
            }

            advertisement.Title = request.Title;
            advertisement.Description = request.Description;
            advertisement.ImageUrl = request.ImageUrl;
            advertisement.JobADVCreatedAt = request.JobADVCreatedAt;
            advertisement.ExpiresAt = request.ExpiresAt;
            advertisement.IsApproved = request.IsApproved;
            advertisement.IsPaid = request.IsPaid;
            advertisement.IsActive = request.IsActive;
            advertisement.DateModified = DateTime.UtcNow;

            // Raise domain event
            advertisement.Raise(new AdvertisementUpdatedEvent(
                advertisement.Id,
                advertisement.Title,
                advertisement.IsApproved,
                advertisement.IsPaid));

            await _repository.AdvertisementRepository.UpdateAsync(advertisement);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

