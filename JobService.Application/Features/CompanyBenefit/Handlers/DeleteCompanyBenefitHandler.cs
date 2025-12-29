using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyBenefit.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Handlers
{
    public class DeleteCompanyBenefitHandler : IRequestHandler<DeleteCompanyBenefitCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteCompanyBenefitHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteCompanyBenefitCommand request, CancellationToken cancellationToken)
        {
            var benefit = await _repository.CompanyBenefit.GetByIdAsync(request.Id);
            if (benefit == null)
            {
                return Result.Fail("CompanyBenefit not found");
            }

            benefit.RaiseDomainEvent(new CompanyBenefitRemovedEvent(
                benefit.Id,
                benefit.CompanyId,
                DateTime.UtcNow));

            await _repository.CompanyBenefit.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

