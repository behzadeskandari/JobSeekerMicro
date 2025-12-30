using AdvertisementService.Application.Interfaces;
using AdvertisementService.Domain.Entities;
using AdvertisementService.Persistence.DbContexts;
using AdvertisementService.Persistence.GenericRepository;

namespace AdvertisementService.Persistence.Repository
{
    public class LogRepository : GenericWriteRepository<Log>, ILogRepository
    {
        public LogRepository(AdvertismentDbContext context) : base(context)
        {
        }
    }
}

