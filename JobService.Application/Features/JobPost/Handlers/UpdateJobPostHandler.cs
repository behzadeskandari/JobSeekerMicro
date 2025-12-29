using System;
using System.Threading.Tasks;
using JobService.Application.Features.JobPost.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobPost.Handlers
{
    public class UpdateJobPostHandler : IRequestHandler<UpdateJobPostCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateJobPostHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateJobPostCommand request, CancellationToken cancellationToken)
        {
            var jobPost = await _repository.JobPostsRepository.GetByIdAsync(request.Id);
            if (jobPost == null)
            {
                return Result.Fail("JobPost not found");
            }

            if (!string.IsNullOrEmpty(request.Title))
                jobPost.Title = request.Title;
            if (!string.IsNullOrEmpty(request.Description))
                jobPost.Description = request.Description;
            if (!string.IsNullOrEmpty(request.Requirements))
                jobPost.Requirements = request.Requirements;
            if (request.BenefitId.HasValue)
                jobPost.BenefitId = request.BenefitId.Value;
            if (!string.IsNullOrEmpty(request.Location))
                jobPost.Location = request.Location;
            if (request.Salary != null)
                jobPost.Salary = request.Salary;
            if (request.ExpiresAt.HasValue)
                jobPost.ExpiresAt = request.ExpiresAt;
            if (request.CityId.HasValue)
                jobPost.CityId = request.CityId.Value;
            if (request.JobCategoryId.HasValue)
                jobPost.JobCategoryId = request.JobCategoryId;
            if (request.ProvinceId.HasValue)
                jobPost.ProvinceId = request.ProvinceId;

            jobPost.DateModified = DateTime.UtcNow;

            await _repository.JobPostsRepository.UpdateAsync(jobPost);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

