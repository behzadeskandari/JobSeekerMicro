using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class JobApplicationRepository : GenericWriteRepository<JobApplication>, IJobApplicationRepository
    {
        public JobApplicationRepository(JobDbContext context) : base(context)
        {
        }
    }
}
