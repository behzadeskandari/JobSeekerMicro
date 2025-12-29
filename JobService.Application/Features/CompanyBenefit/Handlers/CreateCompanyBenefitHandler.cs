using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyBenefit.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Handlers
{
    public class CreateCompanyBenefitHandler : IRequestHandler<CreateCompanyBenefitCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateCompanyBenefitHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateCompanyBenefitCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.Company.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            var benefit = new JobService.Domain.Entities.CompanyBenefit
            {
                CompanyId = request.CompanyId,
                Name = request.Name,
                Description = request.Description,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            benefit.RaiseDomainEvent(new CompanyBenefitAddedEvent(
                benefit.Id,
                benefit.CompanyId,
                benefit.Name,
                DateTime.UtcNow));

            await _repository.CompanyBenefit.AddAsync(benefit);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(benefit.Id);
        }
    }
}

