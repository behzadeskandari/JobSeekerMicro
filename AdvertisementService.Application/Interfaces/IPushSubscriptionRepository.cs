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
        public Task<List<AppPushSubscriptions>> GetAllAsync();

        public Task<List<AppPushSubscriptions>> GetByUserIdAsync(string userId);


        public Task<AppPushSubscriptions?> GetByEndpointAsync(string endpoint);

        public Task AddOrUpdateAsync(AppPushSubscriptions subscription);

        public Task RemoveAsync(AppPushSubscriptions subscription);
    }
}
