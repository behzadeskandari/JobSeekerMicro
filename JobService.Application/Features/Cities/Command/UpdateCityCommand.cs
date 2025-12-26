using FluentResults;
using JobSeeker.Shared.Contracts.City;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application.Features.Cities.Command
{
    public record UpdateCityCommand(UpdateCityDto Dto) : IRequest<Result>;
}
