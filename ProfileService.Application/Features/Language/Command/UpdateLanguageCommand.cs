using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace ProfileService.Application.Features.Language.Command
{
    public record UpdateLanguageCommand(Guid Id, Guid ResumeId, string Name, ProficiencyLevelEnum ProficiencyLevel, bool? IsActive) : IRequest<Result<bool>>;
}
