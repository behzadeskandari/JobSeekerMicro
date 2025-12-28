using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Contracts.City;
using JobSeeker.Shared.Kernel.Abstractions;
using JobService.Application.Features.Cities.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application.Features.Cities.Handlers
{
    internal class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, Result<IEnumerable<CityDto>>>
    {
        private readonly IJobUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllCitiesQueryHandler(IJobUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<CityDto>>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _unitOfWork.City.GetAllAsync();
            var cityDto = _mapper.Map<IEnumerable<CityDto>>(cities);
            return Result.Ok(cityDto);
        }
    }

}
