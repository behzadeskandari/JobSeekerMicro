using JobSeeker.Shared.Models;
using JobSeeker.Shared.PushNotifications.Interfaces;
using IdentityService.Persistence.DbContext;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence.Repository
{
    public class PushSubscriptionRepository : IPushSubscriptionRepository
    {
        private readonly ApplicationUserDbContext _context;

        public PushSubscriptionRepository(ApplicationUserDbContext context)
        {
            _context = context;
        }

        public async Task<List<AppPushSubscriptions>> GetAllAsync()
        {
            return await _context.PushSubscriptions.ToListAsync();
        }

        public async Task<List<AppPushSubscriptions>> GetByUserIdAsync(string userId)
        {
            return await _context.PushSubscriptions
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task<AppPushSubscriptions?> GetByEndpointAsync(string endpoint)
        {
            return await _context.PushSubscriptions
                .FirstOrDefaultAsync(s => s.Endpoint == endpoint);
        }

        public async Task AddOrUpdateAsync(AppPushSubscriptions subscription)
        {
            var existing = await _context.PushSubscriptions
                .FirstOrDefaultAsync(s => s.Endpoint == subscription.Endpoint);

            if (existing != null)
            {
                existing.UserId = subscription.UserId;
                existing.P256DH = subscription.P256DH;
                existing.Auth = subscription.Auth;
                existing.ExpirationTime = subscription.ExpirationTime;
                _context.PushSubscriptions.Update(existing);
            }
            else
            {
                await _context.PushSubscriptions.AddAsync(subscription);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(AppPushSubscriptions subscription)
        {
            _context.PushSubscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
        }
    }
}

