using System.Threading.Tasks;
using JobService.Application.Features.Job.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Job.Handlers
{
    public class DeleteJobHandler : IRequestHandler<DeleteJobCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteJobHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _repository.JobsRepository.GetByIdAsync(request.Id);
            if (job == null)
            {
                return Result.Fail("Job not found");
            }

            await _repository.JobsRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

