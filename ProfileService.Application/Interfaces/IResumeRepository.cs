using JobSeeker.Shared.Common.Interfaces;
using ProfileService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Application.Interfaces
{
    public interface IResumeRepository : IWriteRepository<Resume>
    {
    }
}
