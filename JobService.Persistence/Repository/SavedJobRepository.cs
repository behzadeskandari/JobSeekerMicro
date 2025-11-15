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

        public async Task<SavedJob> AddAsync(SavedJob entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<SavedJob> entities)
        {
            await _writeRepository.AddRangeAsync(entities);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = _writeRepository.FindAsync(x => x.Id == (Guid)id);
            if (entity == null)
            {
                return await Task.FromResult(false);
            }
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(SavedJob entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<SavedJob> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public  Task<IEnumerable<SavedJob>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }

        public async Task<bool> ExistsAsync(Expression<Func<SavedJob, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<SavedJob>> FindAsync(Expression<Func<SavedJob, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<SavedJob>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<SavedJob?> GetByIdAsync(object id)
        {

            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<PagedResult<SavedJob>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<SavedJob, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<SavedJob>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<SavedJob, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }

        public IQueryable<SavedJob> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<SavedJob> UpdateAsync(SavedJob entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<SavedJob> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
