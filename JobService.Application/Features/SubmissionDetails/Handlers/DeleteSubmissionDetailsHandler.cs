using System.Threading.Tasks;
using JobService.Application.Features.SubmissionDetails.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Handlers
{
    public class DeleteSubmissionDetailsHandler : IRequestHandler<DeleteSubmissionDetailsCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteSubmissionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteSubmissionDetailsCommand request, CancellationToken cancellationToken)
        {
            var submission = await _repository.SubmissionDetails.GetByIdAsync(request.Id);
            if (submission == null)
            {
                return Result.Fail("SubmissionDetails not found");
            }

            await _repository.SubmissionDetails.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

