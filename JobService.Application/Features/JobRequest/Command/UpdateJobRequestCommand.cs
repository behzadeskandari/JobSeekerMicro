using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace JobService.Application.Features.JobRequest.Command
{
    public class UpdateJobRequestCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? CoverLetter { get; set; }
        public string? ResumeUrl { get; set; }
        public JobRequestStatus? Status { get; set; }
    }
}

