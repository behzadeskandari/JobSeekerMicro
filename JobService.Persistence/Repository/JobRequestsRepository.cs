using JobService.Application.Features.JobRequest.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;

namespace JobService.Persistence.Repository
{
    public class JobRequestsRepository : GenericWriteRepository<JobRequest>, IJobRequestsRepository
    {
        //private readonly GenericReadRepository<JobRequest> _readRepository;
        private readonly GenericWriteRepository<JobRequest> _writeRepository;
        private readonly JobDbContext _writeContext; // You might need this for specific write logic
        public JobRequestsRepository(JobDbContext writeContext) : base(writeContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            _writeRepository = new GenericWriteRepository<JobRequest>(_writeContext);
        }

        public async Task<List<JobRequest>> GetJobRequestByUserId(GetJobRequestsQuery request, CancellationToken cancellationToken)
        {
            List<JobRequest>? jobRequests = null;
            if (!string.IsNullOrEmpty(request.UserId))
            {
                jobRequests = await _writeContext.JobRequests.Where(jr => jr.UserId == request.UserId).ToListAsync();
            }

            if (request.JobPostId.HasValue)
            {
                jobRequests = jobRequests.Where(jr => jr.JobPostId == request.JobPostId.Value).ToList();
            }
            return jobRequests;
        }
    }

}
