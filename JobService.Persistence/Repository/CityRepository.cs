using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class CityRepository : GenericWriteRepository<City>, ICityRepository
    {
        public CityRepository(JobDbContext context) : base(context)
        {
        }
    }
}
