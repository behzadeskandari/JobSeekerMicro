using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Queries
{
    public class GetCompanyFollowByIdQuery : IRequest<JobService.Domain.Entities.CompanyFollow?>
    {
        public int Id { get; set; }
    }
}

