using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertisementService.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AdvertisementService.Persistence.DbContexts
{
    public class AdvertismentDbContextFactory
           : IDesignTimeDbContextFactory<AdvertismentDbContext>
    {
        
        public AdvertismentDbContext CreateDbContext(string[] args)
        {
            // Find the appsettings.json (adjust the path if needed)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "AdvertisementService.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            var builder = new DbContextOptionsBuilder<AdvertismentDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new AdvertismentDbContext(builder.Options);
        }
    }
}
