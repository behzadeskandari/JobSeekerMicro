using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobApplication.Command
{
    public class UpdateJobApplicationCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Status { get; set; }
        public string? CoverLetter { get; set; }
        public string? Notes { get; set; }
    }
}

