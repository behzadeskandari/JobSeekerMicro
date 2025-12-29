using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobPost.Command
{
    public class DeleteJobPostCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

