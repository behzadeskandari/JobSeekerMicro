using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace JobService.Infrastructure.Repository
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
