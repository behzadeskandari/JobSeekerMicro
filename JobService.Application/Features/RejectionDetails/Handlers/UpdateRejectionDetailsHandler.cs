using System;
using System.Threading.Tasks;
using JobService.Application.Features.RejectionDetails.Command;
using JobService.Application.Interfaces;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Handlers
{
    public class UpdateRejectionDetailsHandler : IRequestHandler<UpdateRejectionDetailsCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateRejectionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateRejectionDetailsCommand request, CancellationToken cancellationToken)
        {
            var rejection = await _repository.RejectionDetails.GetByIdAsync(request.Id);
            if (rejection == null)
            {
                return Result.Fail("RejectionDetails not found");
            }

            if (request.RejectionDate.HasValue)
                rejection.RejectionDate = request.RejectionDate.Value;
            if (request.Reason != null)
                rejection.Reason = request.Reason;

            rejection.DateModified = DateTime.UtcNow;

            await _repository.RejectionDetails.UpdateAsync(rejection);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

