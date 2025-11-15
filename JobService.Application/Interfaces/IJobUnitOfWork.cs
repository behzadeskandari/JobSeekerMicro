using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application.Interfaces
{
    public interface IJobUnitOfWork : IDisposable
    {
        IJobPostsRepository JobPostsRepository { get; }
        IJobRequestsRepository JobRequestsRepository { get; }
        IJobRepository JobsRepository { get; }
        ISavedJobRepository SavedJob { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
