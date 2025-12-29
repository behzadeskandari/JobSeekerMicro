using System;
using FluentResults;
using JobSeeker.Shared.Contracts.Enums;
using MediatR;

namespace JobService.Application.Features.Company.Command
{
    public class UpdateCompanyCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public CompanySize? Size { get; set; }
        public string? Logo { get; set; }
        public string? Description { get; set; }
        public string? Industry { get; set; }
        public string? Location { get; set; }
        public string? Website { get; set; }
        public DateTime? FoundedDate { get; set; }
        public bool? IsVerified { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public int? JobCategoryId { get; set; }
        public int? CityId { get; set; }
        public int? ProvinceId { get; set; }
        public decimal? Rating { get; set; }
        public string? LogoUrl { get; set; }
    }
}

