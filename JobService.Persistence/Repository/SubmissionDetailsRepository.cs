using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class SubmissionDetailsRepository : GenericWriteRepository<SubmissionDetails>, ISubmissionDetailsRepository
    {
        public SubmissionDetailsRepository(JobDbContext context) : base(context)
        {
        }
    }
}
