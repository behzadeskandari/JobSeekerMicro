using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Command
{
    public class UpdateRejectionDetailsCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public DateTime? RejectionDate { get; set; }
        public string? Reason { get; set; }
    }
}

