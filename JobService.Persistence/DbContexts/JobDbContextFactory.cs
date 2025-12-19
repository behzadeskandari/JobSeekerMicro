using AdvertisementService.Persistence.DbContexts;
using JobService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Persistence.DbContexts
{
    public class JobDbContextFactory : IDesignTimeDbContextFactory<JobDbContext>
    {
        public JobDbContext CreateDbContext(string[] args)
        {
            // Find the appsettings.json (adjust the path if needed)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "JobService.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            var builder = new DbContextOptionsBuilder<JobDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new JobDbContext(builder.Options);
        }
    }
}
