using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Paged;
using Microsoft.Data.SqlClient;

namespace JobSeeker.Shared.Common.Interfaces
{
    public interface IWriteRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters);

        Task<T?> GetByIdAsync(object id);
        Task<IList<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> GetQueryable(); // For more complex queries
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        // Add specific read-only query methods as needed

        Task<PagedResult<T>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null);
        Task<PaginatedList<T>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? predicate = null);

    }
}
