using System;
using System.Threading.Tasks;
using JobService.Application.Features.Province.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Province.Handlers
{
    public class CreateProvinceHandler : IRequestHandler<CreateProvinceCommand, Result<int>>
    {
        private readonly IJobUnitOfWork _repository;

        public CreateProvinceHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
        {
            var province = new JobService.Domain.Entities.Province
            {
                Id = request.Id,
                Label = request.Label,
                Value = request.Value,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            };

            await _repository.Province.AddAsync(province);
            await _repository.CommitAsync(cancellationToken);

            return Result.Ok(province.Id);
        }
    }
}

