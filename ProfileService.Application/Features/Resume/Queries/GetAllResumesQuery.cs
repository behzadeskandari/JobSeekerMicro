using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Features.Resume.Queries
{
    public record GetAllResumesQuery : IRequest<List<ProfileService.Domain.Entities.Resume>>;
}
