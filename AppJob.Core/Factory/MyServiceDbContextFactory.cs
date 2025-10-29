using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppJob.Core.Data;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AppJob.Core.Factory
{
    public class MyServiceDbContextFactory : IDesignTimeDbContextFactory<MyServiceDbContext>
    {
        public MyServiceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()

                .SetBasePath(Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory())))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnectionEmailFileDb");

            var optionsBuilder = new DbContextOptionsBuilder<MyServiceDbContext>();
            optionsBuilder.UseSqlServer(connectionString); // Replace with your database provider

            return new MyServiceDbContext(optionsBuilder.Options);
        }
    }
}
