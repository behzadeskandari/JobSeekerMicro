using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Command
{
    public class DeleteRejectionDetailsCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}

