using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Contracts.Paged;
using JobSeeker.Shared.Kernel.Abstractions;
using Microsoft.Data.SqlClient;

namespace JobSeeker.Shared.Common.Interfaces
{
    public interface IWriteRepository<TAggregateRoot> where TAggregateRoot : class , IAggregateRoot
    {
        Task<TAggregateRoot> AddAsync(TAggregateRoot entity);
        Task AddRangeAsync(IEnumerable<TAggregateRoot> entities);
        Task<TAggregateRoot> UpdateAsync(TAggregateRoot entity);
        Task UpdateRangeAsync(IEnumerable<TAggregateRoot> entities);
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(TAggregateRoot entity);
        Task<bool> DeleteRangeAsync(IEnumerable<TAggregateRoot> entities);

        Task<IEnumerable<TAggregateRoot>> ExecuteStoredProcedureAsync(string procedureName, params SqlParameter[] parameters);

        Task<TAggregateRoot?> GetByIdAsync(object id);
        Task<IEnumerable<TAggregateRoot>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TAggregateRoot>> FindAsync(Expression<Func<TAggregateRoot, bool>> expression);
        IQueryable<TAggregateRoot> GetQueryable(); // For more complex queries
        Task<bool> ExistsAsync(Expression<Func<TAggregateRoot, bool>> expression);
        // Add specific read-only query methods as needed

        Task<PagedResult<TAggregateRoot>> GetPagedAsync(int pageNumber, int pageSize, Expression<Func<TAggregateRoot, bool>>? predicate = null);
        Task<PaginatedList<TAggregateRoot>> GetPagedListAsync(int pageNumber, int pageSize, Expression<Func<TAggregateRoot, bool>>? predicate = null);

    }
}
