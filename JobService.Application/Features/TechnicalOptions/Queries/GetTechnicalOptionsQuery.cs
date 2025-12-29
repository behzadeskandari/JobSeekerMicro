using System.Collections.Generic;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.TechnicalOptions.Queries
{
    public class GetTechnicalOptionsQuery : IRequest<IEnumerable<TechnicalOption>>
    {
    }
}

