using System.Threading.Tasks;

namespace AdvertisementService.Application.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync();
    }
}

