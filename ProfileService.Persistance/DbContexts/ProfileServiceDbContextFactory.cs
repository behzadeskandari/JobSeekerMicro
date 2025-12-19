using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Persistance.DbContexts
{
    internal class ProfileServiceDbContextFactory : IDesignTimeDbContextFactory<ProfileServiceDbContext>
    {
        public ProfileServiceDbContext CreateDbContext(string[] args)
        {
            // Find the appsettings.json (adjust the path if needed)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "ProfileService.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            var builder = new DbContextOptionsBuilder<ProfileServiceDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new ProfileServiceDbContext(builder.Options);
        }
    }
}
