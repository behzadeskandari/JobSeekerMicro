using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobService.Persistence.DbContexts;

namespace JobService.Persistence.UnitOfWork
{
    public class JobUnitOfWork : IJobUnitOfWork
    {
        private readonly JobDbContext _context;
        public IJobPostsRepository JobPostsRepository { get; }
        public IJobRequestsRepository JobRequestsRepository { get; }

        public JobUnitOfWork(JobDbContext context)
        {
            _context = context;
            JobPostsRepository = new JobPostsRepository(_context);
            JobRequestsRepository = new JobRequestsRepository(_context);
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => _context.Dispose();
    }
}
