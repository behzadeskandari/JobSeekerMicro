using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
using ProfileService.Persistance.DbContexts;
using ProfileService.Persistance.GenericRepository;

namespace ProfileService.Persistance.Repository
{
    public class LogRepository : GenericWriteRepository<Log>, ILogRepository
    {
        public LogRepository(ProfileServiceDbContext context) : base(context)
        {
        }
    }
}

