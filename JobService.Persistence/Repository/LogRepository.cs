using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class LogRepository : GenericWriteRepository<Log>, ILogRepository
    {
        public LogRepository(JobDbContext context) : base(context)
        {
        }
    }
}

