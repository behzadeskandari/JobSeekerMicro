using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Persistence.DbContext;
using IdentityService.Persistence.GenericRepository;
using Microsoft.EntityFrameworkCore;


namespace IdentityService.Persistence.Repository
{
    public class UserRepository : GenericWriteRepository<User>, IUserRepository
    {
        private readonly ApplicationUserDbContext _context;
        public UserRepository(ApplicationUserDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

    }
}
