using System;
using System.Collections.Generic;
using FluentResults;
using JobService.Domain.ValueObjects;
using MediatR;

namespace JobService.Application.Features.JobPost.Command
{
    public class UpdateJobPostCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Requirements { get; set; }
        public int? BenefitId { get; set; }
        public string? Location { get; set; }
        public SalaryRange? Salary { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public int? CityId { get; set; }
        public int? JobCategoryId { get; set; }
        public int? ProvinceId { get; set; }
    }
}

