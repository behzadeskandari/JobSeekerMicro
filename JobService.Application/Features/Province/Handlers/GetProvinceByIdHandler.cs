using System.Threading.Tasks;
using JobService.Application.Features.Province.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Province.Handlers
{
    public class GetProvinceByIdHandler : IRequestHandler<GetProvinceByIdQuery, JobService.Domain.Entities.Province?>
    {
        private readonly IJobUnitOfWork _repository;

        public GetProvinceByIdHandler(IJobUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<JobService.Domain.Entities.Province?> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.Province.GetByIdAsync(request.Id);
        }
    }
}

