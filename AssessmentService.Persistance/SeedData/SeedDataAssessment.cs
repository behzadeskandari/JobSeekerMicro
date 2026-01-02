using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssessmentService.Domain.Entities;
using AssessmentService.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AssessmentService.Persistance.SeedData
{
    public static class SeedDataAssessment
    {
        public static Task ApplicationStart(IServiceProvider serviceProvider, int maxRetryCount = 5)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<AssessmentDbContext>();
            }
            return Task.CompletedTask;
        }

        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AssessmentDbContext>();

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
                                //    "INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20251219101721_mig_001', '8.0.0')");
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
            catch (Exception)
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
            catch (Exception)
            {
                // _logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }

        private static async Task TrySeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AssessmentDbContext>();

                // Seed PersonalityTraits
                if (!context.PersonalityTraits.Any())
                {
                    var traits = new List<PersonalityTrait>
                    {
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Extraversion", 
                            Description = "تمایل به جستجوی تحریک و لذت بردن از همراهی دیگران.", 
                            TraitType = "Interpersonal",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Agreeableness", 
                            Description = "تمایل به دلسوزی و همکاری با دیگران.", 
                            TraitType = "Interpersonal",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Conscientiousness", 
                            Description = "توانایی سازماندهی، مسئولیت پذیری و هدف گذاری.", 
                            TraitType = "Work",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Openness", 
                            Description = "گرایش به خلاقیت، کنجکاوی و گشودگی به تجربیات جدید.", 
                            TraitType = "Cognitive",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Neuroticism", 
                            Description = "تمایل به تجربه احساسات منفی مانند اضطراب و تحریک پذیری.", 
                            TraitType = "Emotional Stability",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Resilience", 
                            Description = "توانایی بازیابی سریع از مشکلات و سازگاری با تغییرات.", 
                            TraitType = "Work",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Leadership", 
                            Description = "توانایی الهام بخشیدن، تأثیرگذاری و هدایت مؤثر دیگران.", 
                            TraitType = "Leadership",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Communication", 
                            Description = "اثربخشی در بیان افکار و گوش دادن فعال به دیگران.", 
                            TraitType = "Interpersonal",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Critical Thinking", 
                            Description = "توانایی تحلیل و ارزیابی عینی مسائل یا ایده‌ها.", 
                            TraitType = "Cognitive",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTrait 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "Adaptability", 
                            Description = "تمایل و توانایی سازگاری با موقعیت‌ها یا محیط‌های جدید.", 
                            TraitType = "Work",
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        }
                    };
                    context.PersonalityTraits.AddRange(traits);
                    await context.SaveChangesAsync();
                }

                // Seed PersonalityTestItems
                if (!context.PersonalityTestItems.Any())
                {
                    var traitsDictionary = context.PersonalityTraits
                        .GroupBy(t => t.Name)
                        .ToDictionary(g => g.Key, g => g.First().Id);

                    foreach (var key in traitsDictionary.Keys)
                    {
                        Console.WriteLine($"Trait Key: {key}");
                    }

                    var testItems = new List<PersonalityTestItem>
                    {
                        // Extraversion
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "EXT1", 
                            ItemText = "از اینکه مرکز توجه باشم لذت می‌برم.", 
                            ScoringDirection = "Positive",
                            Description = "سنجش جامعه‌پذیری",
                            PersonalityTraitId = traitsDictionary["Extraversion"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "EXT2", 
                            Description = "تنهایی را ترجیح می‌دهد", 
                            ItemText = "از جمع‌های بزرگ دوری می‌کنم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Extraversion"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Agreeableness
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "AGR1",
                            Description = "همدلی را می سنجد", 
                            ItemText = "من با احساسات دیگران همدردی می‌کنم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Agreeableness"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "AGR2",
                            Description = "پرخاشگری تحت فشار", 
                            ItemText = "وقتی ناراحتم به مردم توهین می‌کنم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Agreeableness"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Conscientiousness
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "CON1",
                            Description = "قابلیت اطمینان را اندازه گیری می کند", 
                            ItemText = "من وظایف را به موقع انجام می‌دهم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Conscientiousness"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "CON2", 
                            Description = "بی‌نظمی را اندازه‌گیری می‌کند", 
                            ItemText = "من اغلب کارها را ناتمام رها می‌کنم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Conscientiousness"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Openness
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "OPN1",
                            Description = "خلاقیت را می سنجد", 
                            ItemText = "من قوه تخیل قوی و روشنی دارم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Openness"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "OPN2",
                            Description = "ترجیح بر آشنایی", 
                            ItemText = "من روتین را به تنوع ترجیح می‌دهم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Openness"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Neuroticism
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "NEU1",
                            Description = "واکنش‌پذیری عاطفی را اندازه‌گیری می‌کند", 
                            ItemText = "من به راحتی دچار استرس می‌شوم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Neuroticism"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "NEU2",
                            Description = "کنترل عاطفی را اندازه گیری می کند", 
                            ItemText = "من تحت فشار آرام می‌مانم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Neuroticism"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Resilience
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "RES1",
                            Description = "تاب آوری را اندازه گیری می کند", 
                            ItemText = "بعد از شکست‌ها سریع به حالت عادی برمی‌گردم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Resilience"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "RES2", 
                            Description = "معیار پایداری",
                            ItemText = "وقتی اوضاع سخت می‌شود، به راحتی تسلیم می‌شوم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Resilience"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Leadership
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "LDR1", 
                            Description = "ابتکار رهبری را اندازه گیری می کند", 
                            ItemText = "از اینکه مسئولیت موقعیت‌ها را به عهده بگیرم لذت می‌برم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Leadership"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "LDR2", 
                            Description = "از نقش های رهبری اجتناب می کند", 
                            ItemText = "من ترجیح می‌دهم به جای رهبری، دنباله‌رو باشم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Leadership"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Communication
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "COM1", 
                            Description = "ارتباط کلامی", 
                            ItemText = "من به راحتی می‌توانم منظورم را به روشنی بیان کنم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Communication"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "COM2", 
                            Description = "مشکل در بیان", 
                            ItemText = "من برای توضیح افکارم تقلا می‌کنم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Communication"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Critical Thinking
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "CRT1",
                            Description = "تفکر تحلیلی", 
                            ItemText = "از حل مسائل پیچیده لذت می‌برم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Critical Thinking"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "CRT2", 
                            Description = "از تجزیه و تحلیل اجتناب می کند", 
                            ItemText = "از کارهایی که نیاز به تفکر عمیق دارند، اجتناب می‌کنم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Critical Thinking"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },

                        // Adaptability
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "ADP1", 
                            Description = "انعطاف پذیری", 
                            ItemText = "من به راحتی با موقعیت‌های جدید سازگار می‌شوم.", 
                            ScoringDirection = "Positive", 
                            PersonalityTraitId = traitsDictionary["Adaptability"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        },
                        new PersonalityTestItem 
                        { 
                            Id = Guid.NewGuid(),
                            Name = "ADP2", 
                            Description = "مقاومت در برابر تغییر", 
                            ItemText = "از تغییرات غیرمنتظره متنفرم.", 
                            ScoringDirection = "Negative", 
                            PersonalityTraitId = traitsDictionary["Adaptability"],
                            DateCreated = DateTime.UtcNow,
                            IsActive = true
                        }
                    };

                    context.PersonalityTestItems.AddRange(testItems);
                    await context.SaveChangesAsync();
                }

                // Seed MBTIQuestions
                if (!context.MBTIQuestions.Any())
                {
                    var mbtiQuestions = new List<MBTIQuestions>
                    {
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از حضور در اجتماعات بزرگ لذت می‌برید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید زمان خود را به تنهایی یا با یک یا دو دوست نزدیک بگذرانید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به راحتی با افراد جدید آشنا می‌شوید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا اغلب به دنبال زمان‌هایی برای تنهایی و تفکر می‌گردید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا هنگام کار گروهی انرژی بیشتری دارید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید به تنهایی روی پروژه‌های خود کار کنید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از صحبت کردن در جمع لذت می‌برید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا اغلب احساس می‌کنید نیاز به زمان‌های استراحت از جمع دارید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا بیشتر از برقراری تماس تلفنی ترجیح می‌دهید پیام دهید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید در جمع بزرگ بازی کنید یا بنشینید و تماشا کنید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از حضور در مهمانی‌های شلوغ لذت می‌برید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از صحبت کردن با افراد جدید اضطراب دارید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از انجام فعالیت‌های گروهی انرژی می‌گیرید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید زمان خود را با تفکر و برنامه‌ریزی برای آینده بگذرانید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا اغلب در ملاقات‌های اجتماعی فعال هستید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا بعد از حضور در اجتماعات بزرگ احساس خستگی می‌کنید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از ایجاد ارتباط با دیگران لذت می‌برید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از حضور در مکان‌های آرام و ساکت لذت می‌برید؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از ملاقات و گفتگو با افراد مختلف انرژی می‌گیرید؟", Category = "E", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید وقت خود را در خانه بگذرانید تا در جمع؟", Category = "I", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به جزئیات و حقایق عینی اهمیت می‌دهید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا اغلب در مورد احتمالات و آینده فکر می‌کنید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید با اطلاعات قابل لمس و مستند کار کنید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال الگوها و معانی عمیق‌تر در امور هستید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به جزئیات عملی کارها علاقه‌مند هستید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا اغلب به ایده‌های خلاقانه و نوآورانه فکر می‌کنید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به زمان حال و آنچه که واقعاً اتفاق می‌افتد اهمیت می‌دهید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به نظریه‌ها و احتمالات بیشتر از حقایق موجود اهمیت می‌دهید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال جزئیات دقیق و ملموس هستید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال احتمالات و تغییرات بلندمدت هستید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به واقعیت‌های موجود و حقایق جاری توجه دارید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید به معانی و الگوهای پنهان توجه کنید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به مشاهدات عینی و جزئیات توجه دارید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال ایده‌های جدید و احتمالاً ناملموس هستید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید با اطلاعات واقعی و قابل اعتماد کار کنید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال معانی عمیق‌تر و مفاهیم پنهان هستید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از کار با اطلاعات دقیق و واقعی لذت می‌برید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال الگوها و نظریه‌های پنهان در امور هستید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال حقایق و اطلاعات ملموس هستید؟", Category = "S", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال احتمالات و ایده‌های آینده‌نگرانه هستید؟", Category = "N", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا هنگام تصمیم‌گیری بیشتر به منطق و تحلیل توجه می‌کنید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در تصمیم‌گیری‌ها احساسات و ارزش‌های شخصی را در نظر می‌گیرید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به قوانین و اصول منطقی پایبند هستید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در روابط شخصی به همدلی و تفاهم اهمیت می‌دهید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به واقعیت‌های موجود بیشتر از احساسات شخصی توجه می‌کنید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در موقعیت‌های اجتماعی به احساسات دیگران حساس هستید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید مسائل را به روش عینی و بی‌طرفانه بررسی کنید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در مواجهه با مشکلات به دنبال راه حل‌هایی هستید که همه را راضی کند؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به منطق و استدلال توجه می‌کنید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به احساسات و همدلی توجه می‌کنید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به اصول منطقی و عینی پایبند هستید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در روابط شخصی به احساسات و تفاهم اهمیت می‌دهید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به واقعیت‌ها و حقایق بیشتر از احساسات توجه می‌کنید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در موقعیت‌های اجتماعی به احساسات دیگران توجه دارید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید مسائل را به روش عینی و منطقی بررسی کنید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در مواجهه با مشکلات به دنبال راه حل‌های همدلانه هستید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به منطق و واقعیت توجه می‌کنید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در تصمیم‌گیری‌ها بیشتر به احساسات و ارزش‌ها توجه می‌کنید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به اصول عینی و منطقی پایبند هستید؟", Category = "T", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا در روابط شخصی به همدلی و درک متقابل اهمیت می‌دهید؟", Category = "F", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا دوست دارید برنامه‌ریزی کنید و به برنامه‌ها پایبند باشید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید انعطاف‌پذیر باشید و برنامه‌ها را به راحتی تغییر دهید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از داشتن یک برنامه زمانی دقیق و مشخص لذت می‌برید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال فرصت‌هایی برای تجربه‌های جدید و غیرمنتظره هستید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید کارها را بر اساس یک برنامه مشخص انجام دهید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از انعطاف‌پذیری و آزادی در انجام کارها لذت می‌برید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا دوست دارید کارها را به ترتیب و منظم انجام دهید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید کارها را به طور طبیعی و بدون برنامه‌ریزی دقیق انجام دهید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا دوست دارید برنامه‌ریزی کنید و به آن پایبند بمانید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید برنامه‌ریزی انعطاف‌پذیر و بدون محدودیت داشته باشید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از داشتن یک برنامه مشخص و مدون لذت می‌برید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا به دنبال فرصت‌های جدید و پیش‌بینی نشده هستید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید کارها را بر اساس برنامه انجام دهید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از انعطاف‌پذیری و آزادی در برنامه‌ریزی لذت می‌برید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا دوست دارید کارها را به ترتیب و منظم انجام دهید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید کارها را به طور طبیعی و بدون برنامه‌ریزی دقیق انجام دهید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از برنامه‌ریزی دقیق و مدون لذت می‌برید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا از انعطاف‌پذیری و آزادی در انجام کارها لذت می‌برید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا دوست دارید کارها را به ترتیب و منظم انجام دهید؟", Category = "J", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIQuestions { Id = Guid.NewGuid(), QuestionText = "آیا ترجیح می‌دهید کارها را به طور طبیعی و بدون برنامه‌ریزی دقیق انجام دهید؟", Category = "P", DateCreated = DateTime.UtcNow, IsActive = true }
                    };

                    await context.MBTIQuestions.AddRangeAsync(mbtiQuestions);
                    await context.SaveChangesAsync();
                }

                // Seed MBTIResults
                if (!context.MBTIResults.Any())
                {
                    var mbtiResults = new List<MBTIResult>
                    {
                        new MBTIResult { Id = Guid.NewGuid(), Name = "INTJ", Type = "معمار", Description = "متفکران استراتژیک و خیال‌پرداز با برنامه‌ای برای هر چیز.", Result = "متفکران خلاق و استراتژیک با برنامه برای همه چیز.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "INTP", Type = "متفکر", Description = "مخترعان نوآور با عطشی بی‌پایان برای دانش.", Result = "مخترعان نوآور با عطش سیری‌ناپذیر برای دانش.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ENTJ", Type = "رهبر", Description = "رهبران جسور، تخیلی و اراده‌ای قوی که همیشه راهی یا می‌سازند یا می‌یابند.", Result = "رهبرانی جسور، خلاق و با اراده که همیشه یا راهی پیدا می‌کنند یا راهی می‌سازند.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ENTP", Type = "مبتکر", Description = "افرادی باهوش و کنجکاو که به دنبال ایده‌های جدید و چالش‌های فکری هستند.", Result = "متفکران باهوش و کنجکاوی که نمی‌توانند در برابر یک چالش فکری مقاومت کنند.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "INFJ", Type = "مشاور", Description = "الهام‌بخش و آرمان‌گرا با توانایی قوی در درک دیگران و ایجاد تغییرات مثبت.", Result = "آرمان‌گرایانی آرام و عرفانی، در عین حال بسیار الهام‌بخش و خستگی‌ناپذیر.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "INFP", Type = "شفادهنده", Description = "افراد ساکت و شاعرانه با دیدگاه‌های اصیل و قلب‌های مهربان.", Result = "مردمی شاعر، مهربان و نوع‌دوست، همیشه مشتاق کمک به یک هدف خوب.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ENFJ", Type = "مربی", Description = "رهبران کاریزماتیک و الهام‌بخش که قادر به هیجان و تأثیرگذاری بر دیگران هستند.", Result = "رهبران کاریزماتیک و الهام‌بخش، قادر به مسحور کردن شنوندگان خود.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ENFP", Type = "مبارز", Description = "روح‌های خلاق، پرشور و اجتماعی که همیشه دلیل برای لبخند پیدا می‌کنند.", Result = "افرادی پرشور، خلاق و اجتماعی که همیشه دلیلی برای لبخند زدن پیدا می‌کنند.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ISTJ", Type = "بازرس", Description = "افراد عمل‌گرا، واقع‌گرا و مسئولیت‌پذیر که به سنت‌ها و قوانین پایبند هستند.", Result = "افرادی عمل‌گرا و واقع‌بین که در قابل اعتماد بودنشان شکی نیست.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ISFJ", Type = "مدافع", Description = "محافظان ساکت و مهربان که همیشه آماده کمک به دیگران هستند.", Result = "محافظانی بسیار فداکار و خونگرم، همیشه آماده دفاع از عزیزانشان.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ESTJ", Type = "مدیر", Description = "مدیران عملی و عمل‌گرا که به سازمان‌دهی و مدیریت امور علاقه‌مند هستند.", Result = "مدیران عالی، بی‌رقیب در مدیریت چیزها - یا افراد.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ESFJ", Type = "مشوق", Description = "افرادی اجتماعی و محبوب که همیشه به دنبال هماهنگی و همکاری با دیگران هستند.", Result = "افرادی فوق‌العاده دلسوز، اجتماعی و محبوب، همیشه مشتاق کمک کردن.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ISTP", Type = "صنعتگر", Description = "ماجراجویان عملی و آرام که از اکتشاف و استفاده از ابزارها و ماشین‌ها لذت می‌برند.", Result = "آزمایشگران جسور و عملگرا، استادان انواع ابزارها.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ISFP", Type = "هنرمند", Description = "افراد ساکت، حساس و ملایم که به دنبال خلق زیبایی در جهان هستند.", Result = "هنرمندانی انعطاف‌پذیر و جذاب، همیشه آماده‌ی کشف و تجربه‌ی چیزهای جدید.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ESTP", Type = "ترویج‌دهنده", Description = "افراد فعال، پرشور و پرانرژی که به دنبال زندگی در لحظه هستند.", Result = "افرادی باهوش، پرانرژی و بسیار تیزبین که واقعاً از زندگی در شرایط بحرانی لذت می‌برند.", DateCreated = DateTime.UtcNow, IsActive = true },
                        new MBTIResult { Id = Guid.NewGuid(), Name = "ESFP", Type = "سرگرم‌کننده", Description = "افراد شاد و مشتاق که به دنبال لذت و تفریح در زندگی هستند.", Result = "هنرمندان خودجوش، پرانرژی و مشتاق - زندگی در کنار آنها هرگز کسل کننده نیست.", DateCreated = DateTime.UtcNow, IsActive = true }
                    };

                    await context.MBTIResults.AddRangeAsync(mbtiResults);
                    await context.SaveChangesAsync();
                }

                // Seed PsychologyTests
                if (!context.PsychologyTests.Any())
                {
                    // Enable IDENTITY_INSERT to allow explicit ID values starting from 1
                    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.PsychologyTests', RESEED, 0)");
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.PsychologyTests ON");

        var psychologyTests = new List<PsychologyTest>
        {
            new PsychologyTest
            {
                Id = 1,
                Name = "تست شغلی هلند",
                Description = "علایق شغلی را در 6 تیپ شخصیتی (RIASEC) شناسایی می‌کند.",
                Type = JobSeeker.Shared.Contracts.Enums.PsychologyTestType.Holland,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            },
            new PsychologyTest
            {
                Id = 2,
                Name = "پنج ویژگی شخصیتی بزرگ (NEO-PI-R)",
                Description = "شخصیت را در پنج بُعد اصلی ارزیابی می‌کند (OCEAN).",
                Type = JobSeeker.Shared.Contracts.Enums.PsychologyTestType.BigFive,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            },
            new PsychologyTest
            {
                Id = 3,
                Name = "ارزیابی دیسک",
                Description = "بر رفتار در محیط‌های کاری تمرکز دارد: تسلط، نفوذ، ثبات و وظیفه‌شناسی.",
                Type = JobSeeker.Shared.Contracts.Enums.PsychologyTestType.DISC,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            },
            new PsychologyTest
            {
                Id = 4,
                Name = "هوش هیجانی (EQ-i)",
                Description = "آگاهی عاطفی، کنترل، همدلی و مهارت‌های اجتماعی را ارزیابی می‌کند.",
                Type = JobSeeker.Shared.Contracts.Enums.PsychologyTestType.EmotionalIntelligence,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            },
            new PsychologyTest
            {
                Id = 5,
                Name = "آزمون استعدادهای شناختی",
                Description = "هوش عمومی، منطق، ریاضی و استدلال کلامی را می‌سنجد.",
                Type = JobSeeker.Shared.Contracts.Enums.PsychologyTestType.Cognitive,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            },
            new PsychologyTest
            {
                Id = 6,
                Name = "آزمون قضاوت موقعیتی (SJT)",
                Description = "تصمیم‌گیری در سناریوهای مرتبط با شغل را ارزیابی می‌کند.",
                Type = JobSeeker.Shared.Contracts.Enums.PsychologyTestType.SJT,
                DateCreated = DateTime.UtcNow,
                IsActive = true
            }
        };

        await context.PsychologyTests.AddRangeAsync(psychologyTests);
        await context.SaveChangesAsync();
        // Disable IDENTITY_INSERT after successful seeding
        await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.PsychologyTests OFF");


                }

                // Seed AnswerOptions
                if (!context.AnswerOption.Any())
                {
                    var answerOptions = new List<AnswerOption>
                    {
                        // تست هالند (PsychologyTestId = 1)
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 1, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 1, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 1, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 1, DateCreated = DateTime.UtcNow, IsActive = true },

                        // تست پنج عامل بزرگ شخصیت (Big Five) (PsychologyTestId = 2)
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 2, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 2, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 2, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 2, DateCreated = DateTime.UtcNow, IsActive = true },

                        // تست DISC (PsychologyTestId = 3)
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 3, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 3, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 3, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 3, DateCreated = DateTime.UtcNow, IsActive = true },

                        // تست هوش هیجانی (PsychologyTestId = 4)
                        new AnswerOption { Value = 1, Label = "کاملاً مخالفم", PsychologyTestId = 4, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 2, Label = "مخالفم", PsychologyTestId = 4, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 3, Label = "موافقم", PsychologyTestId = 4, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 4, Label = "کاملاً موافقم", PsychologyTestId = 4, DateCreated = DateTime.UtcNow, IsActive = true },

                        // تست استعداد شناختی (PsychologyTestId = 5)
                        new AnswerOption { Value = 1, Label = "درست است", PsychologyTestId = 5, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 2, Label = "نادرست است", PsychologyTestId = 5, DateCreated = DateTime.UtcNow, IsActive = true },

                        // تست قضاوت موقعیتی (PsychologyTestId = 6)
                        new AnswerOption { Value = 1, Label = "بسیار نامناسب", PsychologyTestId = 6, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 2, Label = "نامناسب", PsychologyTestId = 6, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 3, Label = "مناسب", PsychologyTestId = 6, DateCreated = DateTime.UtcNow, IsActive = true },
                        new AnswerOption { Value = 4, Label = "بسیار مناسب", PsychologyTestId = 6, DateCreated = DateTime.UtcNow, IsActive = true }
                    };

                    await context.AnswerOption.AddRangeAsync(answerOptions);
                    await context.SaveChangesAsync();
                }

                // Seed PsychologyTestQuestions (with AnswerOptions relationships)
                if (!context.PsychologyTestQuestions.Any())
                {
                    // Get AnswerOptions grouped by PsychologyTestId
                    var answerOptionsByTest = context.AnswerOption
                        .Where(ao => ao.IsActive == true)
                        .GroupBy(ao => ao.PsychologyTestId)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    var psychologyTestQuestions = new List<PsychologyTestQuestion>();

                    // Helper to get answer options for a test
                    var getAnswerOptions = (int testId) => 
                        answerOptionsByTest.ContainsKey(testId) 
                            ? answerOptionsByTest[testId] 
                            : new List<AnswerOption>();

                    // Holland Career Test (TestId = 1)
                    var hollandAnswers = getAnswerOptions(1);
                    psychologyTestQuestions.AddRange(new[]
                    {
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من دوست دارم روی ماشین کار کنم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من دوست دارم پازل درست کنم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من در کار مستقل خوب هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من دوست دارم رهبری یک گروه را بر عهده بگیرم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من از نوشتن خلاقانه لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من ترجیح می‌دهم در فضای باز کار کنم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من دوست دارم به مردم در مشکلاتشان کمک کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من از سازماندهی چیزهایی مثل فایل‌ها و گزارش‌ها لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "از تلاش برای تأثیرگذاری یا متقاعد کردن مردم لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من دوست دارم چیزهایی را با دستانم بسازم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من از تعمیر وسایل برقی لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من کارهای ساختارمند را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من به خلاقیت در کار اهمیت می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "از حل کردن مسائل ریاضی لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "دوست دارم در یادگیری به دیگران کمک کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "از کار با ابزار لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من شغلی با دستورالعمل‌های واضح را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من دوست دارم مردم را متقاعد کنم که با من موافق باشند.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من از باغبانی یا محوطه سازی لذت می برم.", QuestionType = "RatingScale", CorrectAnswer = "R", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 1, QuestionText = "من عاشق آزمایش کردن و کشف چیزهای جدید هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = hollandAnswers }
                    });

                    // Big Five Personality Test (TestId = 2)
                    var bigFiveAnswers = getAnswerOptions(2);
                    psychologyTestQuestions.AddRange(new[]
                    {
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من به راحتی با دیگران ارتباط برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من احساسات دیگران را به خوبی درک می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من معمولاً برای رسیدن به اهدافم برنامه ریزی می کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من اغلب احساس اضطراب یا نگرانی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "به تجربه‌های جدید و متفاوت علاقه‌مندم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "در موقعیت‌های اجتماعی فعال و پرانرژی هستم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من به نیازهای دیگران توجه زیادی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من وظایفم را به موقع انجام می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من به راحتی دچار استرس می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = " من از هنر، موسیقی و ادبیات لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من اغلب شروع کننده گفتگو با دیگران هستم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من نسبت به احساسات اطرافیانم حساس هستم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من آدم مسئولیت پذیری هستم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من نوسانات خلقی مکرری را تجربه می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من پذیرای ایده‌های جدید هستم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "از گذراندن وقت با دیگران لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من به نیازهای دیگران توجه دارم.", QuestionType = "RatingScale", CorrectAnswer = "A", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من همیشه کارهای ناتمام را تمام می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "من به راحتی دچار استرس می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 2, QuestionText = "از یادگیری چیزهای جدید لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "O", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = bigFiveAnswers }
                    });

                    // DISC Personality Test (TestId = 3)
                    var discAnswers = getAnswerOptions(3);
                    psychologyTestQuestions.AddRange(new[]
                    {
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "در موقعیت‌های گروهی مسئولیت را به عهده می‌گیرم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من از الهام بخشیدن به دیگران با ایده‌هایم لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من یک محیط کاری ثابت و قابل پیش‌بینی را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من در کارم خیلی به جزئیات اهمیت می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من روی رسیدن سریع به نتایج تمرکز می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "از معاشرت و شبکه‌سازی لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من برای هماهنگی و همکاری تیمی ارزش قائلم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من قوانین و رویه‌ها را به دقت دنبال می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من اهل رقابت و هدف هستم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من مشتاق پروژه‌های جدید هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من یک روال ثابت را ترجیح می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من از صحت کارم اطمینان دارم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من سریع تصمیم می‌گیرم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "از انگیزه دادن به دیگران لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من در شرایط سخت صبور هستم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من کارم را برای یافتن خطاها دو بار بررسی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "از ریسک کردن برای رسیدن به اهداف لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "D", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من در محیط‌های گروهی خوش‌بین هستم.", QuestionType = "RatingScale", CorrectAnswer = "I", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من برای وفاداری در تیم‌ها ارزش قائلم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 3, QuestionText = "من در کارم کیفیت را در اولویت قرار می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "C", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = discAnswers }
                    });

                    // Emotional Intelligence Test (TestId = 4)
                    var emotionalAnswers = getAnswerOptions(4);
                    psychologyTestQuestions.AddRange(new[]
                    {
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من از احساساتم به محض وقوع آنها آگاه هستم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من می‌توانم تکانه‌هایم را به طور مؤثر کنترل کنم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من برای رسیدن به اهدافم انگیزه دارم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "می‌توانم حس کنم دیگران چه احساسی دارند.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من روابط قوی با دیگران برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من نقاط قوت و ضعف خودم را تشخیص می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من تحت فشار آرام می‌مانم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من به کارم علاقه و اشتیاق دارم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من دیدگاه‌های دیگران را درک می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من به طور مؤثر با دیگران ارتباط برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من به واکنش‌های احساسی‌ام فکر می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من در مواقع اختلاف و درگیری، احساساتم را مدیریت می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من با وجود شکست‌ها انگیزه‌ام را حفظ می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من نسبت به دیگران همدلی نشان می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من اختلافات را به طور سازنده حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من محرک‌های احساسی‌ام را درک می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SA", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من رفتارم را با موقعیت‌ها تطبیق می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "SR", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من با اشتیاق اهداف را دنبال می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "M", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من به طور فعال به دیگران گوش می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "E", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 4, QuestionText = "من در تیم‌ها به خوبی همکاری می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "SS", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = emotionalAnswers }
                    });

                    // Cognitive Ability Test (TestId = 5)
                    var cognitiveAnswers = getAnswerOptions(5);
                    psychologyTestQuestions.AddRange(new[]
                    {
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "می‌توانم الگوها را در مسائل پیچیده تشخیص دهم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من در حل مسائل عددی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من دستورالعمل‌های پیچیده‌ی کتبی را می‌فهمم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من می‌توانم اشیاء را در فضای سه‌بعدی تجسم کنم.", QuestionType = "RatingScale", CorrectAnswer = "triad", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من به سرعت معماهای استدلال منطقی را حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من در محاسبات ذهنی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من به راحتی می‌توانم مفاهیم انتزاعی را تفسیر کنم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من می‌توانم اشیاء را به طور ذهنی و با دقت بچرخانم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من در حل مسائل تحلیلی عالی هستم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من می‌توانم درصدها را سریع محاسبه کنم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من در درک قیاس‌های کلامی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من می‌توانم روابط فضایی را به خوبی تجسم کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من مسائل پیچیده را به صورت سیستماتیک حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من در استدلال عددی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "می‌توانم دستورالعمل‌های شفاهی دقیق را درک کنم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من در کارهای استدلال فضایی مهارت دارم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "از مسائل منطقی چالش‌برانگیز لذت می‌برم.", QuestionType = "RatingScale", CorrectAnswer = "L", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من می‌توانم محاسبات ذهنی سریع انجام دهم.", QuestionType = "RatingScale", CorrectAnswer = "N", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من متون نوشتاری پیچیده را می‌فهمم.", QuestionType = "RatingScale", CorrectAnswer = "V", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 5, QuestionText = "من می‌توانم معماهای فضایی را به طور مؤثر حل کنم.", QuestionType = "RatingScale", CorrectAnswer = "S", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = cognitiveAnswers }
                    });

                    // Situational Judgment Test (SJT) (TestId = 6)
                    var sjtAnswers = getAnswerOptions(6);
                    psychologyTestQuestions.AddRange(new[]
                    {
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "وقتی یکی از اعضای تیم، ضرب‌الاجل را از دست می‌دهد، من فوراً و به طور سازنده به آن رسیدگی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Leadership", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "هنگام حل اختلافات در محل کار، آرامش خود را حفظ می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Conflict Resolution", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من وظایف را به طور مؤثر در مهلت‌های محدود اولویت‌بندی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Time Management", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من برای دستیابی به اهداف تیمی با همکارانم همکاری می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Teamwork", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من در موقعیت‌های چالش‌برانگیز، تصمیمات اخلاقی می‌گیرم.", QuestionType = "RatingScale", CorrectAnswer = "Ethics", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من در طول جلسات تیمی به طور واضح ارتباط برقرار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Communication", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من با تغییرات غیرمنتظره در برنامه‌های پروژه سازگار می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "Adaptability", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من به همکاران بازخورد سازنده ارائه می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Leadership", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من در طول بحث‌های مربوط به اختلاف نظر، فعالانه گوش می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Conflict Resolution", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من زمانم را طوری مدیریت می‌کنم که به مهلت‌های پروژه برسم.", QuestionType = "RatingScale", CorrectAnswer = "Time Management", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من به طور مؤثر در پروژه‌های تیمی مشارکت می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Teamwork", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من در تصمیم‌گیری‌ها به اصول اخلاقی پایبند هستم.", QuestionType = "RatingScale", CorrectAnswer = "Ethics", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من ایده‌ها را به روشنی برای ذینفعان بیان می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Communication", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من به سرعت با اولویت‌های کاری جدید سازگار می‌شوم.", QuestionType = "RatingScale", CorrectAnswer = "Adaptability", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من در طول چالش‌ها به تیمم انگیزه می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Leadership", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من اختلافات را با انصاف حل می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Conflict Resolution", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من کارها را برای بهینه‌سازی بهره‌وری اولویت‌بندی می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Time Management", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من در محیط‌های مشارکتی خوب کار می‌کنم.", QuestionType = "RatingScale", CorrectAnswer = "Teamwork", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من تصمیماتی می‌گیرم که با ارزش‌های شرکت همسو باشند.", QuestionType = "RatingScale", CorrectAnswer = "Ethics", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers },
                        new PsychologyTestQuestion { Id = Guid.NewGuid(), PsychologyTestId = 6, QuestionText = "من رویکردم را با چالش‌های جدید تطبیق می‌دهم.", QuestionType = "RatingScale", CorrectAnswer = "Adaptability", ScoringWeight = 1.0m, IsActive = true, DateCreated = DateTime.UtcNow, DateModified = DateTime.UtcNow, AnswerOptions = sjtAnswers }
                    });

                    await context.PsychologyTestQuestions.AddRangeAsync(psychologyTestQuestions);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

