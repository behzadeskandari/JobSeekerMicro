using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Application.Interfaces;
using AdvertisementService.Persistence.DbContexts;
using AdvertisementService.Persistence.Repository;
using Polly;

namespace AdvertisementService.Persistence.UnitOfWork
{
    public class AdvertisementUnitOfWork
    {
        private readonly AdvertismentDbContext _context;
        IAdvertisementRepository AdvertisementRepository { get; }

        public AdvertisementUnitOfWork(AdvertismentDbContext context)
        {
            _context = context;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => _context.Dispose();
    }
}
