using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Persistence.GenericRepository;
using JobSeeker.Shared.Common.Interfaces;
using JobSeeker.Shared.Kernel.Abstractions;
using JobService.Application.Features.JobRequest.Queries;
using JobService.Domain.Entities;

namespace JobService.Application.Interfaces
{
    public interface IJobRequestsRepository :
      IWriteRepository<JobRequest>, IAggregateRoot
    {
        Task<List<JobRequest>> GetJobRequestByUserId(GetJobRequestsQuery request, CancellationToken cancellationToken);
    }
}
