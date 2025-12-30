using ProfileService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace ProfileService.Application.Interfaces
{
    public interface IExceptionLogRepository : IWriteRepository<ExceptionLog>
    {
    }
}

