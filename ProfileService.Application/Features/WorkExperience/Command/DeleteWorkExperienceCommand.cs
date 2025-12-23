using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.WorkExperience.Command
{
    public record DeleteWorkExperienceCommand(Guid Id) : IRequest<Result<string>>;
}
