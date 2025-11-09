using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Common.GenericRepo;
using JobService.Persistence.DbContexts;
using JobSeeker.Shared.Kernel.Abstractions;

namespace JobService.Persistence.GenericRepository
{
    public class GenericWriteRepository<T> : GenericRepository<T>, IWriteRepository<T> where T : class , IAggregateRoot
    {
        private readonly JobDbContext _context;

        public GenericWriteRepository(JobDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
