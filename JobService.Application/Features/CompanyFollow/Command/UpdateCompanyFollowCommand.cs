using FluentResults;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Command
{
    public class UpdateCompanyFollowCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public int? Rating { get; set; }
    }
}

