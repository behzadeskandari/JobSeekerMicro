using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.RejectionDetails.Command
{
    public class CreateRejectionDetailsCommand : IRequest<Result<int>>
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public string RejectedById { get; set; } = string.Empty;

        [Required]
        public DateTime RejectionDate { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}

