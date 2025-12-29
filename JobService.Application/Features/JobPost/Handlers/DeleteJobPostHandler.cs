using System.Threading.Tasks;
using JobService.Application.Features.JobPost.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobPost.Handlers
{
    public class DeleteJobPostHandler : IRequestHandler<DeleteJobPostCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteJobPostHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteJobPostCommand request, CancellationToken cancellationToken)
        {
            var jobPost = await _repository.JobPostsRepository.GetByIdAsync(request.Id);
            if (jobPost == null)
            {
                return Result.Fail("JobPost not found");
            }

            await _repository.JobPostsRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

