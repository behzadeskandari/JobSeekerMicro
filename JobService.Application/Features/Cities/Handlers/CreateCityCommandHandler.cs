using FluentResults;
using JobSeeker.Shared.Contracts.City;
using JobSeeker.Shared.Kernel.Abstractions;
using JobService.Application.Features.Cities.Command;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application.Features.Cities.Handlers
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, Result>
    {
        private readonly IJobUnitOfWork _unitOfWork;

        public CreateCityCommandHandler(IJobUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var city = new CityDto
            {
                Label = request.Dto.Label,
                ProvinceId = request.Dto.ProvinceId,
                IsActive = true
            };
            var cityRecord = new City
            {
                Label = request.Dto.Label,
                ProvinceId = request.Dto.ProvinceId,
                IsActive = true,
                Value = request.Dto.Value,
            };
            await _unitOfWork.City.AddAsync(cityRecord);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
