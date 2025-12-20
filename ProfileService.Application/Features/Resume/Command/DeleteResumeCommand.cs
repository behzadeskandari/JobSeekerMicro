using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.Resume.Command
{
    public record DeleteResumeCommand(Guid Id) : IRequest<Result<string>>;
}
