using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyBenefit.Command
{
    public class DeleteCompanyBenefitCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

