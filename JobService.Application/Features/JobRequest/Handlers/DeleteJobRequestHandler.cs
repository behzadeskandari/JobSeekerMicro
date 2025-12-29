using System.Threading.Tasks;
using JobService.Application.Features.JobRequest.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobRequest.Handlers
{
    public class DeleteJobRequestHandler : IRequestHandler<DeleteJobRequestCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteJobRequestHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteJobRequestCommand request, CancellationToken cancellationToken)
        {
            var jobRequest = await _repository.JobRequestsRepository.GetByIdAsync(request.Id);
            if (jobRequest == null)
            {
                return Result.Fail("JobRequest not found");
            }

            await _repository.JobRequestsRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

