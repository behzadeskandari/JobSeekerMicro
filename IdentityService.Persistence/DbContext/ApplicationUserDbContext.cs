using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence.DbContext
{
    public class ApplicationUserDbContext : IdentityDbContext<User>
    {
        public ApplicationUserDbContext(DbContextOptions options): base(options)
        {
            
        }
    } 
}
