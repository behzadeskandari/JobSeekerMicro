using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Command
{
    public class UpdateSubmissionDetailsCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? Source { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
    }
}

