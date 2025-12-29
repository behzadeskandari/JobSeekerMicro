using System.Threading.Tasks;
using JobService.Application.Features.JobApplication.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobApplication.Handlers
{
    public class DeleteJobApplicationHandler : IRequestHandler<DeleteJobApplicationCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteJobApplicationHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteJobApplicationCommand request, CancellationToken cancellationToken)
        {
            var jobApplication = await _repository.JobApplication.GetByIdAsync(request.Id);
            if (jobApplication == null)
            {
                return Result.Fail("JobApplication not found");
            }

            await _repository.JobApplication.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

