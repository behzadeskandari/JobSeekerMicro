using System;
using System.Threading.Tasks;
using JobService.Application.Features.TechnicalOptions.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Handlers
{
    public class CreateTechnicalOptionHandler : IRequestHandler<CreateTechnicalOptionCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateTechnicalOptionHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateTechnicalOptionCommand request, CancellationToken cancellationToken)
        {
            var technicalOption = new TechnicalOption
            {
                Label = request.Label,
                Value = request.Value,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.TechnicalOptions.AddAsync(technicalOption);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(technicalOption.Id);
        }
    }
}

