using System.Threading.Tasks;
using JobService.Application.Features.CompanyFollow.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.CompanyFollow.Handlers
{
    public class GetCompanyFollowByIdHandler : IRequestHandler<GetCompanyFollowByIdQuery, JobService.Domain.Entities.CompanyFollow?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetCompanyFollowByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.CompanyFollow?> Handle(GetCompanyFollowByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.CompanyFollow.GetByIdAsync(request.Id);
        }
    }
}

