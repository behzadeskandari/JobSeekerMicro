using IdentityService.Persistence.DbContext;
using JobSeeker.Shared.Common.GenericRepo;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Kernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Persistence.GenericRepository
{
    public class GenericWriteRepository<T> : GenericRepository<T>, IWriteRepository<T> where T : class, IAggregateRoot
    {
        private readonly ApplicationUserDbContext _context;

        public GenericWriteRepository(ApplicationUserDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
