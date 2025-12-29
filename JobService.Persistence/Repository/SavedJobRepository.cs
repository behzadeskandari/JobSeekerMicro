using System.Collections.Generic;
using JobService.Application.Features.SavedJob.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class SavedJobRepository : GenericWriteRepository<SavedJob>, ISavedJobRepository
    {
        private readonly GenericWriteRepository<SavedJob> _writeRepository;
        private readonly JobDbContext _writeContext; // You might need this for specific read logic
        public SavedJobRepository(JobDbContext writeContext) : base(writeContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<JobPost>(_readContext);
            _writeRepository = new GenericWriteRepository<SavedJob>(_writeContext);
        }

        public async Task<IEnumerable<SavedJob>> GetSavedJobAsync(GetSavedJobsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<SavedJob> savedJobs = new List<SavedJob>();
            if (!string.IsNullOrEmpty(request.UserId))
            {
                savedJobs = _writeContext.SavedJob.Where(sj => sj.UserId == request.UserId);
            }

            if (request.JobPostId.HasValue)
            {
                savedJobs = savedJobs.Where(sj => sj.JobPostId == request.JobPostId.Value);
            }
            return savedJobs.AsEnumerable();
        }
    }
}
