using System;
using System.Threading.Tasks;
using JobService.Application.Features.Company.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Company.Handlers
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateCompanyHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.Company.GetByIdAsync(request.Id);
            if (company == null)
            {
                return Result.Fail("Company not found");
            }

            if (!string.IsNullOrEmpty(request.Name))
                company.Name = request.Name;
            if (request.Size.HasValue)
                company.Size = request.Size.Value;
            if (request.Logo != null)
                company.Logo = request.Logo;
            if (request.Description != null)
                company.Description = request.Description;
            if (request.Industry != null)
                company.Industry = request.Industry;
            if (request.Location != null)
                company.Location = request.Location;
            if (request.Website != null)
                company.Website = request.Website;
            if (request.FoundedDate.HasValue)
                company.FoundedDate = request.FoundedDate.Value;
            if (request.IsVerified.HasValue)
                company.IsVerified = request.IsVerified.Value;
            if (request.ContactEmail != null)
                company.ContactEmail = request.ContactEmail;
            if (request.ContactPhone != null)
                company.ContactPhone = request.ContactPhone;
            if (request.JobCategoryId.HasValue)
                company.JobCategoryId = request.JobCategoryId;
            if (request.CityId.HasValue)
                company.CityId = request.CityId;
            if (request.ProvinceId.HasValue)
                company.ProvinceId = request.ProvinceId;
            if (request.Rating.HasValue)
                company.Rating = request.Rating.Value;
            if (request.LogoUrl != null)
                company.LogoUrl = request.LogoUrl;

            company.DateModified = DateTime.UtcNow;

            company.RaiseDomainEvent(new CompanyUpdatedEvent(
                company.Id,
                company.Name,
                DateTime.UtcNow));

            await _repository.Company.UpdateAsync(company);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

