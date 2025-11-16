using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

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
    }

}
