using AssessmentService.Persistance.DbContexts;
using JobSeeker.Shared.Common.GenericRepo;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Kernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Persistance.GenericRepository
{
    public class GenericWriteRepository<T> : GenericRepository<T>, IWriteRepository<T> where T : class, IAggregateRoot
    {
        private readonly AssessmentDbContext _context;

        public GenericWriteRepository(AssessmentDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
