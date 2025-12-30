using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using AssessmentService.Persistance.DbContexts;
using AssessmentService.Persistance.GenericRepository;

namespace AssessmentService.Persistance.Repository
{
    public class LogRepository : GenericWriteRepository<Log>, ILogRepository
    {
        public LogRepository(AssessmentDbContext context) : base(context)
        {
        }
    }
}

