using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class TechnicalOptionsRepository : GenericWriteRepository<TechnicalOption>, ITechnicalOptionsRepository
    {
        public TechnicalOptionsRepository(JobDbContext context) : base(context)
        {
        }
    }
}
