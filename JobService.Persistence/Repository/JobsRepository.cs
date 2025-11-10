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
    public class JobsRepository : IJobsRepository
    {
        private readonly GenericWriteRepository<Job> _writeRepository;
        private readonly JobDbContext _writeContext; // You might need this for specific read logic
        public JobsRepository(JobDbContext writeContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<JobPost>(_readContext);
            _writeRepository = new GenericWriteRepository<Job>(_writeContext);
        }
        public async Task<Job> AddAsync(Job entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<Job> entities)
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

        public async Task<bool> DeleteAsync(Job entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<Job> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public Task<IEnumerable<Job>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }

        public async Task<bool> ExistsAsync(Expression<Func<Job, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<Job>> FindAsync(Expression<Func<Job, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<Job>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Job?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<PagedResult<Job>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<Job, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<Job>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<Job, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }

        public IQueryable<Job> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<Job> UpdateAsync(Job entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<Job> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }
    }
}
