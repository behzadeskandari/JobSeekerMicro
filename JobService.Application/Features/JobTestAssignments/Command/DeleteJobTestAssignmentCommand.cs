using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobTestAssignments.Command
{
    public class DeleteJobTestAssignmentCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

