using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SubmissionDetails.Command
{
    public class DeleteSubmissionDetailsCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

