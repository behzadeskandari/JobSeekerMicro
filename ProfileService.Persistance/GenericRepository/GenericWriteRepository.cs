using JobSeeker.Shared.Common.GenericRepo;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Kernel.Abstractions;
using ProfileService.Persistance.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Persistance.GenericRepository
{
    public class GenericWriteRepository<T> : GenericRepository<T>, IWriteRepository<T> where T : class, IAggregateRoot
    {
        private readonly ProfileServiceDbContext _context;

        public GenericWriteRepository(ProfileServiceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
