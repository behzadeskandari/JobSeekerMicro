using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProfileService.Application.Features.Education.Command
{
    public record CreateEducationCommand(
       Guid ResumeId,
       string Degree,
       string Institution,
       string Field,
       DateTime StartDate,
       DateTime? EndDate,
       string Description) : IRequest<Guid>;
}
