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
    public class OutBoxRepository : GenericWriteRepository<OutboxMessage>, IOutboxMessage
    {
        public OutBoxRepository(ApplicationUserDbContext context) : base(context)
        {
        }
    }
}
