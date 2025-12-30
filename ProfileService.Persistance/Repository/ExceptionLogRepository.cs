using ProfileService.Application.Interfaces;
using ProfileService.Domain.Entities;
using ProfileService.Persistance.DbContexts;
using ProfileService.Persistance.GenericRepository;

namespace ProfileService.Persistance.Repository
{
    public class ExceptionLogRepository : GenericWriteRepository<ExceptionLog>, IExceptionLogRepository
    {
        public ExceptionLogRepository(ProfileServiceDbContext context) : base(context)
        {
        }
    }
}

