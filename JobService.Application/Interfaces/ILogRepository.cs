using JobService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace JobService.Application.Interfaces
{
    public interface ILogRepository : IWriteRepository<Log>
    {
    }
}

