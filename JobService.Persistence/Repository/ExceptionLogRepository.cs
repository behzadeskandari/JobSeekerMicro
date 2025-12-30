using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;

namespace JobService.Persistence.Repository
{
    public class ExceptionLogRepository : GenericWriteRepository<ExceptionLog>, IExceptionLogRepository
    {
        public ExceptionLogRepository(JobDbContext context) : base(context)
        {
        }
    }
}

