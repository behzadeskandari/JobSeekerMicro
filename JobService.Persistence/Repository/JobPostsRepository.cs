using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class JobPostsRepository : GenericWriteRepository<JobPost>, IJobPostsRepository
    {
        public JobPostsRepository(JobDbContext context) : base(context) { }

        public async Task<IEnumerable<JobPost>> GetAllWithSkillsAsync()
        {
            //:TODO
            //return await _dbSet.Include(j => j.Skills).ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<JobPost> GetByIdWithSkillsAsync(Guid jobId)
        {
            //:TODO
            throw new NotImplementedException();

            //return await _dbSet.Include(j => j.Skills)
            //                   .FirstOrDefaultAsync(j => j.Id == jobId);
        }
    }
}
