using System;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyBenefit.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Handlers
{
    public class UpdateCompanyBenefitHandler : IRequestHandler<UpdateCompanyBenefitCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateCompanyBenefitHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateCompanyBenefitCommand request, CancellationToken cancellationToken)
        {
            var benefit = await _repository.CompanyBenefit.GetByIdAsync(request.Id);
            if (benefit == null)
            {
                return Result.Fail("CompanyBenefit not found");
            }

            if (!string.IsNullOrEmpty(request.Name))
                benefit.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Description))
                benefit.Description = request.Description;

            benefit.DateModified = DateTime.UtcNow;

            await _repository.CompanyBenefit.UpdateAsync(benefit);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

