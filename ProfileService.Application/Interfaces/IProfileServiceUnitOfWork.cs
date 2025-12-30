using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Application.Interfaces
{
    public interface IProfileServiceUnitOfWork : IDisposable
    {
        public ICandidateJobPreferencesRepository CandidateJobPreferencesRepository { get; }
        public ICandidateRepository CandidateRepository { get; }
        public IEducationRepository EducationRepository { get; }
        public ILanguageRepository LanguageRepository { get; }
        public IResumeRepository ResumeRepository { get; }
        public ISkillRepository SkillRepository { get; }
        public IUserSettingsRepository UserSettingsRepository { get; }
        public IWorkExperienceRepository WorkExperienceRepository { get; }
        ILogRepository Logs { get; }
        IExceptionLogRepository ExceptionLogs { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
