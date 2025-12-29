using System;
using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Command
{
    public class CreateOfferDetailsCommand : IRequest<Result<int>>
    {
        [Required]
        public int ApplicationId { get; set; }

        [Required]
        public string OfferedById { get; set; } = string.Empty;

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public DateTime OfferDate { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public string Currency { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
    }
}

