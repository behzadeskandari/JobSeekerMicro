using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Command
{
    public class UpdateCompanyBenefitCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

