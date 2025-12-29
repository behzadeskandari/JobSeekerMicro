using JobService.Domain.Entities;
using MediatR;

namespace JobService.Application.Features.Province.Queries
{
    public class GetProvinceByIdQuery : IRequest<JobService.Domain.Entities.Province?>
    {
        public int Id { get; set; }
    }
}

