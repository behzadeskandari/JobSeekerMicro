using System;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace JobService.Application.Features.Job.Command
{
    public class UpdateJobCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public JobLevel? Level { get; set; }
        public int? CompanyId { get; set; }
        public bool? IsProirity { get; set; }
        public JobType? JobType { get; set; }
        public string? JobDescription { get; set; }
        public string? JobRequirment { get; set; }
        public int? CityId { get; set; }
        public int? TechnicalOptionsId { get; set; }
        public int? JobCategoryId { get; set; }
        public JobOfferStatus? Status { get; set; }
    }
}

