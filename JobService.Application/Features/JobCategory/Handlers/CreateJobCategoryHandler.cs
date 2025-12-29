using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobCategory.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobCategory.Handlers
{
    public class CreateJobCategoryHandler : IRequestHandler<CreateJobCategoryCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateJobCategoryHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new JobService.Domain.Entities.JobCategory
            {
                Name = request.Name,
                NameEn = request.NameEn,
                Slug = request.Slug,
                Industry = request.Industry,
                Value = request.Value,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            category.RaiseDomainEvent(new JobCategoryCreatedEvent(
                category.Id,
                category.Name,
                category.Industry,
                DateTime.UtcNow));

            await _repository.JobCategory.AddAsync(category);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(category.Id);
        }
    }
}

