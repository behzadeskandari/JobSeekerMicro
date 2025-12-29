using System;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.OfferDetails.Command
{
    public class UpdateOfferDetailsCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public DateTime? OfferDate { get; set; }
        public decimal? Salary { get; set; }
        public string? Currency { get; set; }
        public string? Benefits { get; set; }
        public string? Status { get; set; }
    }
}

