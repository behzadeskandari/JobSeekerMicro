using JobSeeker.Shared.Common.Interfaces;
using JobService.Application.Interfaces;
using JobService.Persistence.GenericRepository;
using JobService.Persistence.Repository;
using JobService.Persistence.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Persistence
{
    public static class JobPersistanceServiceRegistration
    {
        public static IServiceCollection AddJobPersistanceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped<IJobUnitOfWork, JobUnitOfWork>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICompanyBenefitRepository, CompanyBenefitRepository>();
            services.AddScoped<ICompanyFollowRepository, CompanyFollowRepository>();
            services.AddScoped<ICompanyJobPreferencesRepository, CompanyJobPreferencesRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IInterviewDetailRepository, InterviewDetailRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IJobPostsRepository, JobPostsRepository>();
            services.AddScoped<IJobRequestsRepository, JobRequestsRepository>();
            services.AddScoped<IJobTestAssignmentsRepository, JobTestAssignmentsRepository>();
            services.AddScoped<IJobRepository, JobsRepository>();
            services.AddScoped<IJobTestAssignmentsRepository, JobTestAssignmentsRepository>();
            services.AddScoped<IOfferDetailsRepository, OfferDetailsRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IRejectionDetailsRepository, RejectionDetailsRepository>();
            services.AddScoped<ISavedJobRepository, SavedJobRepository>();
            services.AddScoped<ISubmissionDetailsRepository, SubmissionDetailsRepository>();
            services.AddScoped<ITechnicalOptionsRepository, TechnicalOptionsRepository>();


                

            return services;

        }
    }
}
