using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.Job.Command
{
    public class DeleteJobCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

