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
    public class AdvertisementUnitOfWork : IAdvertisementUnitOfWork
    {
        private readonly AdvertismentDbContext _context;
        public IAdvertisementRepository _advertisementRepository;

        public IAdvertisementRepository AdvertisementRepository => _advertisementRepository ??= new AdvertisementRepository(_context);

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
