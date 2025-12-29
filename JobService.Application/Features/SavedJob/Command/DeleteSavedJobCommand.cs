using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.SavedJob.Command
{
    public class DeleteSavedJobCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

