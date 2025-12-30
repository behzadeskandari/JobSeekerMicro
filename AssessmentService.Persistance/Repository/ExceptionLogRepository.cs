using AssessmentService.Application.Interfaces;
using AssessmentService.Domain.Entities;
using AssessmentService.Persistance.DbContexts;
using AssessmentService.Persistance.GenericRepository;

namespace AssessmentService.Persistance.Repository
{
    public class ExceptionLogRepository : GenericWriteRepository<ExceptionLog>, IExceptionLogRepository
    {
        public ExceptionLogRepository(AssessmentDbContext context) : base(context)
        {
        }
    }
}

