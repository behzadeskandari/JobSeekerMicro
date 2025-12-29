using System.ComponentModel.DataAnnotations;
using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Command
{
    public class CreateCompanyBenefitCommand : IRequest<Result<int>>
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}

