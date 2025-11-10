using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
namespace IdentityService.Persistence.DbContext
{
    public class ApplicationUserDbContextFactory : IDesignTimeDbContextFactory<ApplicationUserDbContext>
    {
        public ApplicationUserDbContext CreateDbContext(string[] args)
        {
            // Find the appsettings.json (adjust the path if needed)
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "IdentityService.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationUserDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new ApplicationUserDbContext(builder.Options);
        }
    }
}
