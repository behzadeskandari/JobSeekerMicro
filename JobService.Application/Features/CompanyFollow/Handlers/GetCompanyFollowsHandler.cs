using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobService.Application.Features.CompanyFollow.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Handlers
{
    public class GetCompanyFollowsHandler : IRequestHandler<GetCompanyFollowsQuery, IEnumerable<JobService.Domain.Entities.CompanyFollow>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompanyFollowsHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.CompanyFollow>> Handle(GetCompanyFollowsQuery request, CancellationToken cancellationToken)
        {
            var follows = await _repository.CompanyFollow.GetByUserIdIdAsync(request.UserId,cancellationToken);
            return follows;
        }
    }
}

