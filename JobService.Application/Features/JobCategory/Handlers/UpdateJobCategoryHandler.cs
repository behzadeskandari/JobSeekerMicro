using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobCategory.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobCategory.Handlers
{
    public class UpdateJobCategoryHandler : IRequestHandler<UpdateJobCategoryCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateJobCategoryHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateJobCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.JobCategory.GetByIdAsync(request.Id);
            if (category == null)
            {
                return Result.Fail("JobCategory not found");
            }

            if (!string.IsNullOrEmpty(request.Name))
                category.Name = request.Name;
            if (request.NameEn != null)
                category.NameEn = request.NameEn;
            if (request.Slug != null)
                category.Slug = request.Slug;
            if (request.Industry != null)
                category.Industry = request.Industry;
            if (request.Value != null)
                category.Value = request.Value;

            category.DateModified = DateTime.UtcNow;

            category.RaiseDomainEvent(new JobCategoryUpdatedEvent(
                category.Id,
                category.Name,
                DateTime.UtcNow));

            await _repository.JobCategory.UpdateAsync(category);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

