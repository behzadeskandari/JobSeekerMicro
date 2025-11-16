using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class JobCategoryRepository : GenericWriteRepository<JobCategory>, IJobCategoryRepository
    {
        public JobCategoryRepository(JobDbContext context) : base(context)
        {
        }
    }
}
