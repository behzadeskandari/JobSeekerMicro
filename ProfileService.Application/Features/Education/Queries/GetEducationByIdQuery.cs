using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Features.Education.Queries
{
    public record GetEducationByIdQuery(int Id) : IRequest<ProfileService.Domain.Entities.Education>;
}
