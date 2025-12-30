using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Persistence.DbContexts;
using AdvertisementService.Persistence.GenericRepository;

namespace AdvertisementService.Persistence.Repository
{
    public class ExceptionLogRepository : GenericWriteRepository<ExceptionLog>, IExceptionLogRepository
    {
        public ExceptionLogRepository(AdvertismentDbContext context) : base(context)
        {
        }
    }
}

