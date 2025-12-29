using System;
using System.Threading.Tasks;
using JobService.Application.Features.OfferDetails.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Handlers
{
    public class UpdateOfferDetailsHandler : IRequestHandler<UpdateOfferDetailsCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateOfferDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateOfferDetailsCommand request, CancellationToken cancellationToken)
        {
            var offer = await _repository.OfferDetails.GetByIdAsync(request.Id);
            if (offer == null)
            {
                return Result.Fail("OfferDetails not found");
            }

            if (request.OfferDate.HasValue)
                offer.OfferDate = request.OfferDate.Value;
            if (request.Salary.HasValue)
                offer.Salary = request.Salary.Value;
            if (request.Currency != null)
                offer.Currency = request.Currency;
            if (request.Benefits != null)
                offer.Benefits = request.Benefits;
            if (!string.IsNullOrEmpty(request.Status))
                offer.Status = request.Status;

            offer.DateModified = DateTime.UtcNow;

            offer.RaiseDomainEvent(new OfferDetailsUpdatedEvent(
                offer.Id,
                offer.ApplicationId,
                offer.Status,
                DateTime.UtcNow));

            await _repository.OfferDetails.UpdateAsync(offer);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

