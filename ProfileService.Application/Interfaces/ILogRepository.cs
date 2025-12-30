using ProfileService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace ProfileService.Application.Interfaces
{
    public interface ILogRepository : IWriteRepository<Log>
    {
    }
}

