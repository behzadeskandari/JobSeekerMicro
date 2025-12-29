using System.Threading.Tasks;
using JobService.Application.Features.JobPost.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobPost.Handlers
{
    public class PublishJobPostHandler : IRequestHandler<PublishJobPostCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public PublishJobPostHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(PublishJobPostCommand request, CancellationToken cancellationToken)
        {
            var jobPost = await _repository.JobPostsRepository.GetByIdAsync(request.Id);
            if (jobPost == null)
            {
                return Result.Fail("JobPost not found");
            }

            jobPost.Publish();

            await _repository.JobPostsRepository.UpdateAsync(jobPost);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

