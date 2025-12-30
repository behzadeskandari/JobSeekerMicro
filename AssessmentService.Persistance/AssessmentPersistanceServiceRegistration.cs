using AssessmentService.Application.Interfaces;
using AssessmentService.Persistance.GenericRepository;
using AssessmentService.Persistance.Repository;
using AssessmentService.Persistance.UnitOfWork;
using JobSeeker.Shared.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentService.Persistance
{
    public static class AssessmentPersistanceServiceRegistration
    {
        public static IServiceCollection AddAssessmentPersistanceServiceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped<IAssessmentServiceUnitOfWork, AssessmentServiceUnitOfWork>();
            services.AddScoped<IAnswerOptionsRepository, AnswerOptionsRepository>();
            services.AddScoped<IMBTIQuestionsRepository, MBTIQuestionsRepository>();
            services.AddScoped<IMBTIResultAnswersRepository, MBTIResultAnswersRepository>();
            services.AddScoped<IMBTIResultsRepository, MBTIResultsRepository>();
            services.AddScoped<IPersonalityTestItemsRepository, PersonalityTestItemsRepository>();
            services.AddScoped<IPersonalityTestResponsesRepository, PersonalityTestResponsesRepository>();
            services.AddScoped<IPersonalityTestResultsRepository, PersonalityTestResultsRepository>();
            services.AddScoped<IPersonalityTraitsRepository, PersonalityTraitsRepository>();
            services.AddScoped<IPsychologyTestInterpretationRepository, PsychologyTestInterpretationRepository>();
            services.AddScoped<IPsychologyTestQuestionsRepository, PsychologyTestQuestionsRepository>();
            services.AddScoped<IPsychologyTestResponseAnswersRepository, PsychologyTestResponseAnswersRepository>();
            services.AddScoped<IPsychologyTestResponsesRepository, PsychologyTestResponsesRepository>();
            services.AddScoped<IPsychologyTestResultAnswersRepository, PsychologyTestResultAnswersRepository>();
            services.AddScoped<IPsychologyTestResultsRepository, PsychologyTestResultsRepository>();
            services.AddScoped<IPsychologyTestsRepository, PsychologyTestsRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IExceptionLogRepository, ExceptionLogRepository>();
            return services;
        }
    }
}
