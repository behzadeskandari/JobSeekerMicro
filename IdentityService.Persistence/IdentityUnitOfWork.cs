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
    public class IdentityUnitOfWork : IIdentityUnitOfWOrk
    {
        private readonly ApplicationUserDbContext _context;
        public IUserRepository _users;
        public IOutboxMessage _outBoxMessge;


        public IUserRepository Users => _users ?? new UserRepository(_context);
        public IOutboxMessage OutboxMessage => _outBoxMessge ?? new OutBoxRepository(_context);

        public IdentityUnitOfWork(ApplicationUserDbContext context, IUserRepository users, IOutboxMessage outBoxMessge)
        {
            _context = context;
            _users = users;
            _outBoxMessge = outBoxMessge;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
