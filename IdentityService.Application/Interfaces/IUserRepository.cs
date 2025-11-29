using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;

namespace IdentityService.Application.Interfaces
{
    public interface IUserRepository : IWriteRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
