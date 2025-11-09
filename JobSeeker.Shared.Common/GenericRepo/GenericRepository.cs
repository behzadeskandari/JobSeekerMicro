using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Contracts.Paged;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using JobSeeker.Shared.Kernel.Abstractions;

namespace JobSeeker.Shared.Common.GenericRepo
{

    public abstract class GenericRepository<T> :
        //IReadRepository<T>,
        IWriteRepository<T> where T : class , IAggregateRoot
    {
        private readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        public IQueryable<T> EntityQueryable { get; private set; }

        protected GenericRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
            //_query = _context.Set<T>().AsQueryable();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            // await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            //await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            // await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            // await _context.SaveChangesAsync();
            return true;
        }
        public virtual async Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            //  await _context.SaveChangesAsync();
            return true;
        }
        //public virtual async Task DeleteRangeAsync(IEnumerable<T> records)
        //{
        //    foreach (var record in records)
        //    {
        //        _dbSet.RemoveRange(record);
        //    }
        //    await _context.SaveChangesAsync();
        //}

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            //   await _context.SaveChangesAsync();
        }

        public async Task<PaginatedList<T>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null)
        {
            var query = predicate is not null ? _dbSet.Where(predicate) : _dbSet;
            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedList<T>(items, totalCount: totalItems, pageNumber, pageSize, totalItems);
        }
        public async Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null)
        {
            var query = predicate is not null ? _dbSet.Where(predicate) : _dbSet;
            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            var sql = $"EXEC {procedureName} {string.Join(", ", parameters.Select(p => p.ParameterName))}";
            return await _dbSet.FromSqlRaw(sql, parameters).ToListAsync();
        }


    }

}
