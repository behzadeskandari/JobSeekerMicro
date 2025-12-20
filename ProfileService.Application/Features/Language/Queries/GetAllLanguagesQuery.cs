using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Features.Language.Queries
{
    public record GetAllLanguagesQuery : IRequest<List<ProfileService.Domain.Entities.Language>>;
}
