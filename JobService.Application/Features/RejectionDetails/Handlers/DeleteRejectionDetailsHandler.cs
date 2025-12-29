using System.Threading.Tasks;
using JobService.Application.Features.RejectionDetails.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Handlers
{
    public class DeleteRejectionDetailsHandler : IRequestHandler<DeleteRejectionDetailsCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public DeleteRejectionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteRejectionDetailsCommand request, CancellationToken cancellationToken)
        {
            var rejection = await _repository.RejectionDetails.GetByIdAsync(request.Id);
            if (rejection == null)
            {
                return Result.Fail("RejectionDetails not found");
            }

            await _repository.RejectionDetails.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

