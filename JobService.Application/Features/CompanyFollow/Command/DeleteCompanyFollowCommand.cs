using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Command
{
    public class DeleteCompanyFollowCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}

