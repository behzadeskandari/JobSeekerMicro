using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Interfaces
{
    public interface IIdentityUnitOfWOrk : IDisposable
    {
        IUserRepository Users { get; }
        IOutboxMessage OutboxMessage { get; }
        Task<int> CommitAsync();
        void Rollback();
    }
}
