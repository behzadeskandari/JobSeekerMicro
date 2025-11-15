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
using Microsoft.EntityFrameworkCore;

namespace JobService.Persistence.Repository
{
    internal class JobPostsRepository : GenericWriteRepository<JobPost>, IJobPostsRepository
    {
        private readonly GenericWriteRepository<JobPost> _writeRepository;
        private readonly JobDbContext _writeContext; // You might need this for specific read logic
        public JobPostsRepository(JobDbContext writeContext) : base(writeContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            //_readRepository = new GenericReadRepository<JobPost>(_readContext);
            _writeRepository = new GenericWriteRepository<JobPost>(_writeContext);
        }

        public async Task<JobPost> AddAsync(JobPost entity)
        {
            await _writeRepository.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<JobPost> entities)
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

        public async Task<bool> DeleteAsync(JobPost entity)
        {
            await _writeRepository.DeleteAsync(entity);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<JobPost> entities)
        {
            await _writeRepository.DeleteRangeAsync(entities);
            return await Task.FromResult(true);
        }

        public async Task<bool> ExistsAsync(Expression<Func<JobPost, bool>> expression)
        {
            return await _writeRepository.ExistsAsync(expression);
        }

        public async Task<IEnumerable<JobPost>> FindAsync(Expression<Func<JobPost, bool>> expression)
        {
            var record = await _writeRepository.FindAsync(expression);
            return record;
        }

        public async Task<IEnumerable<JobPost>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _writeRepository.GetAllAsync(cancellationToken);
        }

        public async Task<IEnumerable<JobPost>> GetAllWithSkillsAsync()
        {
            var records = await _writeRepository.GetQueryable()
                //.Include(jp => jp.Skill)
                .ToListAsync();
            return records;
        }

        public async Task<JobPost?> GetByIdAsync(object id)
        {
            return await _writeRepository.GetByIdAsync(id);
        }

        public async Task<JobPost> GetByIdWithSkillsAsync(Guid jobId)
        {
            return await _writeRepository.GetQueryable().Where(x => x.JobId == jobId).SingleOrDefaultAsync();
        }

        public IQueryable<JobPost> GetQueryable()
        {
            return _writeRepository.GetQueryable();
        }

        public async Task<JobPost> UpdateAsync(JobPost entity)
        {
            var record = await _writeRepository.UpdateAsync(entity);
            return record;
        }

        public async Task UpdateRangeAsync(IEnumerable<JobPost> entities)
        {
            await _writeRepository.UpdateRangeAsync(entities);
        }


        public async Task<PagedResult<JobPost>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<JobPost, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedAsync(pageNumber, pageSize, predicate);
        }

        public async Task<PaginatedList<JobPost>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<JobPost, bool>>? predicate = null)
        {
            return await _writeRepository.GetPagedListAsync(pageNumber, pageSize, predicate);
        }
        public Task<IEnumerable<JobPost>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            return _writeRepository.ExecuteStoredProcedureAsync(procedureName, parameters);
        }
    }
}
