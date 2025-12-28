using AutoMapper;
using FluentResults;
using JobSeeker.Shared.Contracts.City;
using JobService.Application.Features.Cities.Queries;
using JobService.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application.Features.Cities.Handlers
{
    public class GetCityByIdQueryHandler : IRequestHandler<GetCityById, Result<List<CityDto>>>
    {
        private readonly IJobUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetCityByIdQueryHandler(IJobUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<CityDto>>> Handle(GetCityById request, CancellationToken cancellationToken)
        {
            var city = await _repository.City.GetQueryable().Where(x => x.ProvinceId == request.ProvinceId).ToListAsync();
            if (city == null)
                return Result.Fail("شهر پیدا نشد");
            // var dto = _mapper.Map<CityDto>(city);
            var lst = city.Select(x => new CityDto
            {
                Id = x.Id,
                Label = x.Label,
                IsActive = x.IsActive.Value,
                ProvinceId = x.ProvinceId,
                Value = x.Value,
            }).ToList();
            return Result.Ok<List<CityDto>>(lst);
        }
    }
}
