using System;
using System.Threading.Tasks;
using JobService.Application.Features.OfferDetails.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Handlers
{
    public class CreateOfferDetailsHandler : IRequestHandler<CreateOfferDetailsCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateOfferDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateOfferDetailsCommand request, CancellationToken cancellationToken)
        {
            var application = await _repository.JobApplication.GetByIdAsync(request.ApplicationId);
            if (application == null)
            {
                return Result.Fail("JobApplication not found");
            }

            var company = await _repository.Company.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            var offer = new JobService.Domain.Entities.OfferDetails
            {
                ApplicationId = request.ApplicationId,
                OfferedById = request.OfferedById,
                CompanyId = request.CompanyId,
                OfferDate = request.OfferDate,
                Salary = request.Salary,
                Currency = request.Currency,
                Benefits = request.Benefits,
                Status = request.Status,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            offer.RaiseDomainEvent(new OfferDetailsCreatedEvent(
                offer.Id,
                offer.ApplicationId,
                offer.OfferedById,
                offer.CompanyId,
                offer.Salary,
                DateTime.UtcNow));

            await _repository.OfferDetails.AddAsync(offer);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(offer.Id);
        }
    }
}

