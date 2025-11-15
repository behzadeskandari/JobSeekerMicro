using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Paged;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace JobService.Persistence.Repository
{
    public class JobPostsRepository : GenericWriteRepository<JobPost>, IJobPostsRepository
    {
        private readonly GenericWriteRepository<JobPost> _writeRepository;
        private readonly JobDbContext _writeContext; // You might need this for specific read logic
        public JobPostsRepository(JobDbContext writeContext) : base(writeContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<JobPost>(_readContext);
            _writeRepository = new GenericWriteRepository<JobPost>(_writeContext);
        }

        public async Task<IEnumerable<JobPost>> GetAllWithSkillsAsync()
        {
            var records = await _writeRepository.GetQueryable()
                //.Include(jp => jp.Skill)
                .ToListAsync();
            return records;
        }


        public async Task<JobPost> GetByIdWithSkillsAsync(Guid jobId)
        {
            return await _writeRepository.GetQueryable().Where(x => x.JobId == jobId).SingleOrDefaultAsync();
        }

      
    }
}
