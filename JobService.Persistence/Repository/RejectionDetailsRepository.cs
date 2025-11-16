using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class RejectionDetailsRepository : GenericWriteRepository<RejectionDetails>, IRejectionDetailsRepository
    {
        public RejectionDetailsRepository(JobDbContext context) : base(context)
        {
        }
    }
}
