using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Features.WorkExperience.Command
{
    public record CreateWorkExperienceCommand(
       Guid ResumeId,
       string JobTitle,
       string CompanyName,
       bool IsCurrentJob,
       string Description) : IRequest<Guid>;

}
