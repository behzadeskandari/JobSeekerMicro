using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Persistence.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace IdentityService.Persistence.SeedData
{
    public static class SeedDataIdentity
    {
        public static async Task ApplicationStart(IServiceProvider serviceProvider, int maxRetryCount = 5)
        {
            using (var scope = serviceProvider.CreateScope())
            { 
                var _context = scope.ServiceProvider.GetRequiredService<ApplicationUserDbContext>();
                
            }
        }
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationUserDbContext>();

                    if (_context.Database.IsSqlServer())
                    {
                        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
                        if (pendingMigrations.Any())
                        {
                            // Apply migrations only if schema doesn't match
                            var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync();
                            if (!appliedMigrations.Any())
                            {
                                // If no migrations are applied but tables exist, mark initial migration as applied
                                //await _context.Database.ExecuteSqlRawAsync(
                                //    "INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20250428173255_InitialCreate', '8.0.0')");
                            }
                            else
                            {
                                await _context.Database.MigrateAsync();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No pending migrations.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while initializing the database");
                throw;
            }
        }

        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            try
            {
                await TrySeedAsync(serviceProvider);
            }
            catch (Exception ex)
            {
                // _logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }


        private static async Task TrySeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationUserDbContext>();
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new IdentityRole[]
                    {
                        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                        new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
                        new IdentityRole() { Name = "Staff", ConcurrencyStamp = "1", NormalizedName = "Staff" },
                    });
                }
                var ids = await context.SaveChangesAsync();
            }
        }
    }
}
