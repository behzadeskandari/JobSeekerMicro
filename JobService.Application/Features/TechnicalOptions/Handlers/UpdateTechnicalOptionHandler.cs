using System;
using System.Threading.Tasks;
using JobService.Application.Features.TechnicalOptions.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Events;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Handlers
{
    public class UpdateTechnicalOptionHandler : IRequestHandler<UpdateTechnicalOptionCommand, Result>
    {
        private readonly IJobUnitOfWork _repository;

        public UpdateTechnicalOptionHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result> Handle(UpdateTechnicalOptionCommand request, CancellationToken cancellationToken)
        {
            var technicalOption = await _repository.TechnicalOptions.GetByIdAsync(request.Id);
            if (technicalOption == null)
            {
                return Result.Fail("TechnicalOption not found");
            }

            if (!string.IsNullOrEmpty(request.Label))
                technicalOption.Label = request.Label;
            if (!string.IsNullOrEmpty(request.Value))
                technicalOption.Value = request.Value;

            technicalOption.DateModified = DateTime.UtcNow;

            // Note: TechnicalOption is EntityBaseInt (not AuditableEntityBaseInt), so it doesn't have RaiseDomainEvent
            // But if we need to raise events, we should check if it implements IAggregateRoot
            // For now, we'll skip domain events for TechnicalOption as it's a simple lookup table

            await _repository.TechnicalOptions.UpdateAsync(technicalOption);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

