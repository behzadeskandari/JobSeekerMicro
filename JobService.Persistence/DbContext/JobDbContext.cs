using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JobService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobService.Persistence.DbContexts
{
    public class JobDbContext : DbContext
    {
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobRequest> JobRequests { get; set; }

        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options) { }
    }
}
