using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.Language.Command
{
    public record DeleteLanguageCommand(Guid Id) : IRequest<Result<string>>;

}
