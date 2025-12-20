using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Features.Language.Queries
{
    public record GetLanguageByIdQuery(Guid Id) : IRequest<ProfileService.Domain.Entities.Language>;

}
