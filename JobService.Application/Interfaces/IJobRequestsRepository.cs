using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSeeker.Shared.Common.Interfaces;
using JobService.Domain.Entities;

namespace JobService.Application.Interfaces
{
    public interface IJobRequestsRepository :
          IWriteRepository<JobRequest>
    //, IReadRepository<JobSeeker.Domain.Common.Entities.JobRequest>
    {
    }
}
