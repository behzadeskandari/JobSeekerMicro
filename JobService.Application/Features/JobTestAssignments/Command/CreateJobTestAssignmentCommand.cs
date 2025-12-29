using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Command
{
    public class CreateJobTestAssignmentCommand : IRequest<Result<int>>
    {
        public int? JobId { get; set; }
        public int? PsychologyTestId { get; set; }
        public int? PersonalityTestId { get; set; }

        [Required]
        public bool IsRequired { get; set; }
    }
}

