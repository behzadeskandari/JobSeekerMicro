using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace ProfileService.Application.Features.Language.Command
{
    public record CreateLanguageCommand(Guid ResumeId, string Name, ProficiencyLevelEnum ProficiencyLevel) : IRequest<Guid>;
}
