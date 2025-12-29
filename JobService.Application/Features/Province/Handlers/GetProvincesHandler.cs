using System.Collections.Generic;
using System.Threading.Tasks;
using JobService.Application.Features.Province.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Province.Handlers
{
    public class GetProvincesHandler : IRequestHandler<GetProvincesQuery, IEnumerable<JobService.Domain.Entities.Province>>
    {
        private readonly IJobUnitOfWork _repository;

        public GetProvincesHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobService.Domain.Entities.Province>> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Province.GetAllAsync(cancellationToken);
        }
    }
}

