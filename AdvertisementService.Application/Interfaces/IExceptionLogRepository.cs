using AdvertisementService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace AdvertisementService.Application.Interfaces
{
    public interface IExceptionLogRepository : IWriteRepository<ExceptionLog>
    {
    }
}

