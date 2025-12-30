using AssessmentService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace AssessmentService.Application.Interfaces
{
    public interface IExceptionLogRepository : IWriteRepository<ExceptionLog>
    {
    }
}

