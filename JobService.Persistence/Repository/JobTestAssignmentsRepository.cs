using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    internal class JobTestAssignmentsRepository : GenericWriteRepository<JobTestAssignment>, IJobTestAssignmentsRepository
    {
        public JobTestAssignmentsRepository(JobDbContext context) : base(context)
        {
        }
    }
}
