using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace ProfileService.Application.Features.WorkExperience.Command
{
    public record UpdateWorkExperienceCommand(
        Guid Id,
        Guid ResumeId,
        string JobTitle,
        string CompanyName,
        bool IsCurrentJob,
        string Description,
        bool? IsActive) : IRequest<Result<string>>;
}
