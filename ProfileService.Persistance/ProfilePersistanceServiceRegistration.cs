using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileService.Application.Interfaces;
using ProfileService.Persistance.Repository;
using ProfileService.Application.Interfaces;
using ProfileService.Persistance.GenericRepository;
using JobSeeker.Shared.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfileService.Persistance.UnitOfWork;

namespace ProfileService.Persistance
{
    public static class ProfilePersistanceServiceRegistration
    {
        public static IServiceCollection AddProfilePersistanceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped<IProfileServiceUnitOfWork, ProfileServiceUnitOfWork>();
            services.AddScoped<ICandidateJobPreferencesRepository, CandidateJobPreferencesRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IResumeRepository, ResumeRepository>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
            services.AddScoped<IWorkExperienceRepository, WorkExperienceRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();

            return services;
        }
    }
}
