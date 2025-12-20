using IdentityService.Domain.Entities;
using JobSeeker.Shared.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Interfaces
{
    public interface IOutboxMessage : IWriteRepository<OutboxMessage>
    {
    }
}
