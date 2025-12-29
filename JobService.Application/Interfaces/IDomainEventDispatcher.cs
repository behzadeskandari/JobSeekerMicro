using System.Threading.Tasks;

namespace JobService.Application.Interfaces
{
    public interface IDomainEventDispatcher
    {
        Task DispatchDomainEventsAsync();
    }
}

