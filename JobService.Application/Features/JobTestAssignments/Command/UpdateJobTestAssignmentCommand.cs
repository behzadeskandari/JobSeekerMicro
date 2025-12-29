using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Command
{
    public class UpdateJobTestAssignmentCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int? JobId { get; set; }
        public int? PsychologyTestId { get; set; }
        public int? PersonalityTestId { get; set; }
        public bool? IsRequired { get; set; }
    }
}

