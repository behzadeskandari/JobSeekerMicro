using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Paged;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.GenericRepository;
using Microsoft.Data.SqlClient;

namespace JobService.Persistence.Repository
{
    public class SavedJobRepository :GenericWriteRepository<SavedJob>, ISavedJobRepository
    {
        private readonly GenericWriteRepository<SavedJob> _writeRepository;
        private readonly JobDbContext _writeContext; // You might need this for specific read logic
        public SavedJobRepository(JobDbContext writeContext) : base(writeContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<JobPost>(_readContext);
            _writeRepository = new GenericWriteRepository<SavedJob>(_writeContext);
        }

    }
}
