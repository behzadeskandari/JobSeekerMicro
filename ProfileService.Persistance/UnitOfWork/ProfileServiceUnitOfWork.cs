using ProfileService.Application.Interfaces;
using ProfileService.Persistance.DbContexts;
using ProfileService.Persistance.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Persistance.UnitOfWork
{
    public class ProfileServiceUnitOfWork : IProfileServiceUnitOfWork
    {
        private readonly ProfileServiceDbContext _context;
        public ICandidateJobPreferencesRepository _candidateJobPreferencesRepository;
        public ICandidateJobPreferencesRepository CandidateJobPreferencesRepository => _candidateJobPreferencesRepository ??= new CandidateJobPreferencesRepository(_context);

        public ICandidateRepository _candidateRepository;
        public ICandidateRepository CandidateRepository => _candidateRepository ??= new CandidateRepository(_context);

        public IEducationRepository _educationRepository;
        public IEducationRepository EducationRepository => _educationRepository??=new EducationRepository(_context);


        public ILanguageRepository _languageRepository;
        public ILanguageRepository LanguageRepository => _languageRepository ??= new LanguageRepository(_context);

        
        public IResumeRepository _resumeRepository;
        public IResumeRepository ResumeRepository => _resumeRepository ??= new ResumeRepository(_context);

        
        public ISkillRepository _skillRepository;
        public ISkillRepository SkillRepository => _skillRepository ??= new SkillRepository(_context);

       
        public IUserSettingsRepository _userSettingsRepository;
        public IUserSettingsRepository UserSettingsRepository => _userSettingsRepository ??= new UserSettingsRepository(_context);


        public IWorkExperienceRepository _workExperienceRepository;
        public IWorkExperienceRepository WorkExperienceRepository => _workExperienceRepository ??= new WorkExperienceRepository(_context);

        public ILogRepository _logs;
        public ILogRepository Logs => _logs ??= new LogRepository(_context);

        public IExceptionLogRepository _exceptionLogs;
        public IExceptionLogRepository ExceptionLogs => _exceptionLogs ??= new ExceptionLogRepository(_context);

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose() => _context.Dispose();
    }
}
