using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Core;
using AppJob.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppJob.Core.Data
{
    public class MyServiceDbContext : DbContext
    {
        public MyServiceDbContext(DbContextOptions<MyServiceDbContext> options) : base(options)
        {
        }

        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<SMSLog> SMSLogs { get; set; }
        public DbSet<SendResult> SendResults { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }

        public DbSet<GeneratedLink> GeneratedLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GeneratedLink>().HasIndex(l => l.Token).IsUnique();
            modelBuilder.Entity<EmailLog>().HasIndex(e => e.IsSent).;
            // Configure your entities here
            base.OnModelCreating(modelBuilder);
        }
    }
     
}
