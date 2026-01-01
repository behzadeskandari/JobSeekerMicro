using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityService.Domain.Entities;
using IdentityService.Persistence.DbContext;
using JobSeeker.Shared.Models;
using JobSeeker.Shared.Models.Roles;
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
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Seed roles
                if (!context.Roles.Any())
                {
                    context.Roles.AddRange(new IdentityRole[]
                    {
                        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                        new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
                        new IdentityRole() { Name = "Staff", ConcurrencyStamp = "1", NormalizedName = "Staff" },
                    });
                    await context.SaveChangesAsync();
                }

                // Seed user: behzad.b.i.g@gmail.com
                var seedEmail = "behzad.b.i.g@gmail.com";
                var existingUser = await userManager.FindByEmailAsync(seedEmail);
                
                if (existingUser == null)
                {
                    var seedUser = new User
                    {
                        FirstName = "eskandari",
                        LastName = "behzad",
                        Email = seedEmail,
                        UserName = seedEmail,
                        EmailConfirmed = true,
                        Role = AppRoles.Admin
                    };

                    var createResult = await userManager.CreateAsync(seedUser, "Jokernewsmiles1!");
                    
                    if (createResult.Succeeded)
                    {
                        // Add user to Admin role
                        if (await roleManager.RoleExistsAsync(AppRoles.Admin))
                        {
                            await userManager.AddToRoleAsync(seedUser, AppRoles.Admin);
                        }
                    }
                }

                // Seed TermsOfService
                if (!context.TermsOfServices.Any())
                {
                    var termsOfService = new TermsOfService
                    {
                        Version = "1.0",
                        LastUpdated = "2024-05-15",
                        DateCreated = DateTime.UtcNow,
                        IsActive = true,
                    };
                    await context.TermsOfServices.AddAsync(termsOfService);
                    await context.SaveChangesAsync();
                    
                    var sections = new List<TermsSection>
                    {
                        new TermsSection
                        {
                            Id = 1,
                            TermsOfServiceId = termsOfService.Id,
                            Title = "مقدمه",
                            Content = "به پلتفرم کاریابی ما خوش آمدید. این شرایط سرویس، قوانین و مقررات استفاده از خدمات ما را برای کارجویان و کارفرمایان تعیین می‌کند. با استفاده از پلتفرم، شما موافقت خود را با این شرایط اعلام می‌کنید.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 2,
                            TermsOfServiceId = termsOfService.Id,
                            Title = "تعهدات کارجویان",
                            Content = "کارجویان متعهد می‌شوند اطلاعات هویتی و رزومه خود را به صورت دقیق و کامل وارد کنند. هرگونه اطلاعات نادرست، ممکن است منجر به حذف حساب کاربری شود. ارسال رزومه برای موقعیت‌های شغلی نامرتبط، مجاز نیست.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 3,
                            TermsOfServiceId = termsOfService.Id,
                            Title = "تعهدات کارفرمایان",
                            Content = "کارفرمایان متعهد می‌شوند اطلاعات شرکت و فرصت‌های شغلی را به صورت صحیح و شفاف درج کنند. هرگونه آگهی شغلی که شامل تبعیض یا محتوای غیرقانونی باشد، حذف خواهد شد. کارفرمایان باید در زمان مشخص به درخواست‌های ارسالی پاسخ دهند.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 4,
                            TermsOfServiceId = termsOfService.Id,
                            Title = "حریم خصوصی و حفاظت از داده‌ها",
                            Content = "ما متعهد به حفاظت از اطلاعات شخصی کاربران هستیم. اطلاعات جمع‌آوری شده تنها با رضایت صریح شما و به منظور بهبود خدمات، در اختیار طرفین (کارجو و کارفرما) قرار خواهد گرفت. برای اطلاعات بیشتر، لطفاً سیاست حفظ حریم خصوصی ما را مطالعه کنید.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 5,
                            TermsOfServiceId = termsOfService.Id,
                            Title = "مالکیت معنوی",
                            Content = "کلیه حقوق مالکیت معنوی پلتفرم، از جمله لوگو، محتوا و طراحی، متعلق به ما است. هرگونه استفاده غیرمجاز از این موارد پیگرد قانونی دارد.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 6,
                            TermsOfServiceId = termsOfService.Id,
                            Title = "محدودیت مسئولیت",
                            Content = "پلتفرم ما تنها واسطه بین کارجو و کارفرما است. ما هیچگونه مسئولیتی در قبال صحت اطلاعات درج شده توسط کاربران یا نتایج مصاحبه‌ها و استخدام‌ها نداریم. مسئولیت نهایی بر عهده طرفین است.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new TermsSection
                        {
                            Id = 7,
                            TermsOfServiceId = termsOfService.Id,
                            Title = "حل اختلافات",
                            Content = "در صورت بروز هرگونه اختلاف، ابتدا تلاش می‌شود از طریق مذاکره و میانجی‌گری حل و فصل شود. در غیر این صورت، حل اختلاف از طریق مراجع قانونی صالح پیگیری خواهد شد.",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        }
                    };

                    await context.TermsSections.AddRangeAsync(sections);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
