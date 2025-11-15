using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Persistence.DbContexts;
using JobSeeker.Shared.Common.GenericRepo;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Kernel.Abstractions;

namespace AdvertisementService.Persistence.GenericRepository
{
    public class GenericWriteRepository<T> : GenericRepository<T>, IWriteRepository<T> where T : class, IAggregateRoot
    {
        private readonly AdvertismentDbContext _context;

        public GenericWriteRepository(AdvertismentDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
