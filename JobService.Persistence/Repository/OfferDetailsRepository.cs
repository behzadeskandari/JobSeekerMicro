using System.Collections.Generic;
using JobService.Application.Features.OfferDetails.Queries;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class OfferDetailsRepository : GenericWriteRepository<OfferDetails>, IOfferDetailsRepository
    {
        //private readonly GenericReadRepository<JobRequest> _readRepository;
        private readonly GenericWriteRepository<JobRequest> _writeRepository;
        private readonly JobDbContext _writeContext; // You might need this for specific write logic
        public OfferDetailsRepository(JobDbContext writeContext) : base(writeContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            _writeRepository = new GenericWriteRepository<JobRequest>(_writeContext);
        }

        public async Task<IEnumerable<OfferDetails>> GetOfferDetails(GetOfferDetailsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<OfferDetails> offerDetails = new List<OfferDetails>();
            if (request.ApplicationId.HasValue)
            {
                offerDetails = _writeContext.OfferDetails.Where(o => o.ApplicationId == request.ApplicationId.Value).AsEnumerable();
            }

            if (request.CompanyId.HasValue)
            {
                offerDetails = _writeContext.OfferDetails.Where(o => o.CompanyId == request.CompanyId.Value).AsEnumerable();
            }
            return offerDetails;
        }
    }
}
