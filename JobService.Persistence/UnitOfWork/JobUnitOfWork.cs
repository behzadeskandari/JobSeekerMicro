using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobService.Application.Interfaces;
using JobService.Domain.Entities;
using JobService.Persistence.DbContexts;
using JobService.Persistence.Repository;
using Microsoft.AspNetCore.Rewrite;

namespace JobService.Persistence.UnitOfWork
{
    public class JobUnitOfWork : IJobUnitOfWork
    {
        private readonly JobDbContext _context;
        public IJobPostsRepository _jobPostsRepository;
        public IJobRequestsRepository _jobRequestsRepository;

        public IJobRepository _jobsRepository;

        public ISavedJobRepository _savedJob;

        public ICityRepository _city;

        public ICompanyBenefitRepository _companyBenefit;

        public ICompanyFollowRepository _companyFollow;

        public ICompanyJobPreferencesRepository _companyJobPreferences;

        public ICompanyRepository _company;

        public IInterviewDetailRepository _interviewDetail;

        public IJobApplicationRepository _jobApplication;

        public IJobCategoryRepository _jobCategory;

        public IJobTestAssignmentsRepository _jobTestAssignments;

        public IOfferDetailsRepository _offerDetails;

        public IProvinceRepository _province;

        public IRejectionDetailsRepository _rejectionDetails;

        public ISubmissionDetailsRepository _submissionDetails;

        public ITechnicalOptionsRepository _technicalOptions;

        private readonly IDomainEventDispatcher? _domainEventDispatcher;

        public JobUnitOfWork(JobDbContext context, IDomainEventDispatcher? domainEventDispatcher = null)
        {
            _context = context;
            _domainEventDispatcher = domainEventDispatcher;
        }


        public IJobPostsRepository JobPostsRepository => _jobPostsRepository ??= new JobPostsRepository(_context);
        public IJobRequestsRepository JobRequestsRepository => _jobRequestsRepository ??= new JobRequestsRepository(_context);
        public IJobRepository JobsRepository => _jobsRepository ??= new JobsRepository(_context);
        public ISavedJobRepository SavedJob => _savedJob ??= new SavedJobRepository(_context);
        public ICityRepository City => _city ?? new CityRepository(_context);
        public ICompanyBenefitRepository CompanyBenefit => _companyBenefit ?? new CompanyBenefitRepository(_context);
        public ICompanyFollowRepository CompanyFollow => _companyFollow ?? new CompanyFollowRepository(_context);
        public ICompanyJobPreferencesRepository CompanyJobPreferences => _companyJobPreferences ?? new CompanyJobPreferencesRepository(_context);
        public ICompanyRepository Company => _company ?? new CompanyRepository(_context);
        public IInterviewDetailRepository InterviewDetail => _interviewDetail ??= new InterviewDetailRepository(_context);

        public IJobApplicationRepository JobApplication => _jobApplication ??= new JobApplicationRepository(_context);

        public IJobCategoryRepository JobCategory => _jobCategory ??= new JobCategoryRepository(_context);

        public IJobTestAssignmentsRepository JobTestAssignments  => _jobTestAssignments ??= new JobTestAssignmentsRepository(_context);

        public IOfferDetailsRepository OfferDetails => _offerDetails ??= new OfferDetailsRepository(_context);


        public IProvinceRepository Province => _province ?? new ProvinceRepository(_context);

        public IRejectionDetailsRepository RejectionDetails => _rejectionDetails ?? new RejectionDetailsRepository(_context);

        public ISubmissionDetailsRepository SubmissionDetails => _submissionDetails ??= new SubmissionDetailsRepository(_context);

        public ITechnicalOptionsRepository TechnicalOptions => _technicalOptions ??= new TechnicalOptionsRepository(_context);
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            
            // Dispatch domain events after save
            if (_domainEventDispatcher != null)
            {
                await _domainEventDispatcher.DispatchDomainEventsAsync();
                await _context.SaveChangesAsync(cancellationToken); // Save outbox messages
            }
            
            return result;
        }

        public void Dispose() => _context.Dispose();
    }
}
