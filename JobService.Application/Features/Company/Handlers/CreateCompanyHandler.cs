using System;
using System.Threading.Tasks;
using JobService.Application.Features.Company.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Company.Handlers
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, Result<string>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateCompanyHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new JobService.Domain.Entities.Company
            {
                Name = request.Name,
                Size = request.Size,
                Logo = request.Logo,
                Description = request.Description,
                Industry = request.Industry,
                Location = request.Location,
                Website = request.Website,
                FoundedDate = request.FoundedDate,
                IsVerified = request.IsVerified,
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                UserId = request.UserId,
                JobCategoryId = request.JobCategoryId,
                CityId = request.CityId,
                ProvinceId = request.ProvinceId,
                Rating = request.Rating,
                LogoUrl = request.LogoUrl,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.Company.AddAsync(company);
            await _repository.CommitAsync(cancellationToken);

            company.RaiseDomainEvent(new CompanyCreatedEvent(
                company.Id,
                company.Name,
                company.UserId,
                company.LogoUrl,
                DateTime.UtcNow,
                company.IsActive ?? true,
                company.IsVerified));

            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(company.Id.ToString());
        }
    }
}

