using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.JobApplication.Command
{
    public class DeleteJobApplicationCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

