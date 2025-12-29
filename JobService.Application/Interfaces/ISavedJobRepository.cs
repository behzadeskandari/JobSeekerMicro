using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Persistence.GenericRepository;
using JobSeeker.Shared.Common.Interfaces;
using JobService.Application.Features.SavedJob.Queries;
using JobService.Domain.Entities;

namespace JobService.Application.Interfaces
{
    public interface ISavedJobRepository :
          //IReadRepository<SavedJob>,
          IWriteRepository<SavedJob>
    {
        Task<IEnumerable<SavedJob>> GetSavedJobAsync(GetSavedJobsQuery request, CancellationToken cancellationToken);
    }
}
