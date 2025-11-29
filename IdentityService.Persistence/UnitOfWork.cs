using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Application.Interfaces;
using IdentityService.Persistence.DbContext;
using IdentityService.Persistence.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
namespace IdentityService.Persistence
{
    public class UnitOfWork : IIdentityUnitOfWOrk
    {
        private readonly ApplicationUserDbContext _context;
        public IUserRepository _users;
        private IDbContextTransaction? _transaction;


        public IUserRepository Users => _users ?? new UserRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        public void Rollback()
        {
            // If using explicit transactions, rollback here. For simple SaveChanges-based approach this is a no-op.
            _transaction?.Rollback();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
