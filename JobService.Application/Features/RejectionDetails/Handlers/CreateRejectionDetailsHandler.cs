using System;
using System.Threading.Tasks;
using JobService.Application.Features.RejectionDetails.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Handlers
{
    public class CreateRejectionDetailsHandler : IRequestHandler<CreateRejectionDetailsCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateRejectionDetailsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateRejectionDetailsCommand request, CancellationToken cancellationToken)
        {
            var application = await _repository.JobApplication.GetByIdAsync(request.ApplicationId);
            if (application == null)
            {
                return Result.Fail("JobApplication not found");
            }

            var rejection = new JobService.Domain.Entities.RejectionDetails
            {
                //Id = Guid.NewGuid(),
                ApplicationId = request.ApplicationId,
                RejectedById = request.RejectedById,
                RejectionDate = request.RejectionDate,
                Reason = request.Reason,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            rejection.RaiseDomainEvent(new RejectionDetailsCreatedEvent(
                rejection.Id,
                rejection.ApplicationId,
                rejection.RejectedById,
                rejection.Reason,
                DateTime.UtcNow));

            await _repository.RejectionDetails.AddAsync(rejection);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(rejection.Id);
        }
    }
}

