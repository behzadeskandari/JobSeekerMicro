using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace AdvertisementService.Application.Interfaces
{
    public interface ILogRepository : IWriteRepository<Log>
    {
    }
}

