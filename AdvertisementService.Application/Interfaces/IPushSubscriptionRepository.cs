using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Models;

namespace AdvertisementService.Application.Interfaces
{
    public interface IPushSubscriptionRepository
    {
        public Task<List<PushSubscription>> GetAllAsync();

        public Task<List<PushSubscription>> GetByUserIdAsync(string userId);


        public Task<PushSubscription?> GetByEndpointAsync(string endpoint);

        public Task AddOrUpdateAsync(PushSubscription subscription);

        public Task RemoveAsync(PushSubscription subscription);
    }
}
