using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class OfferDetailsRepository : GenericWriteRepository<OfferDetails>, IOfferDetailsRepository
    {
        public OfferDetailsRepository(JobDbContext context) : base(context)
        {
        }
    }
}
