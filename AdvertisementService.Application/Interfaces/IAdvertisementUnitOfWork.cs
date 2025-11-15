using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementService.Application.Interfaces
{
    public interface IAdvertisementUnitOfWork : IDisposable
    {
        IAdvertisementRepository AdvertisementRepository { get; }


        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
