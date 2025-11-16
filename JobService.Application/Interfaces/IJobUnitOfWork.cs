using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobService.Application.Interfaces
{
    public interface IJobUnitOfWork : IDisposable
    {
        ICityRepository City { get; }
        ICompanyBenefitRepository CompanyBenefit { get; }
        ICompanyFollowRepository CompanyFollow { get; }
        ICompanyJobPreferencesRepository CompanyJobPreferences { get; }
        ICompanyRepository Company { get; }
        IInterviewDetailRepository InterviewDetail { get; }
        IJobApplicationRepository JobApplication { get; }
        IJobCategoryRepository JobCategory { get; }
        IJobPostsRepository JobPostsRepository { get; }
        IJobRepository JobsRepository { get; }
        IJobRequestsRepository JobRequestsRepository { get; }
        IJobTestAssignmentsRepository JobTestAssignments { get; }
        IOfferDetailsRepository OfferDetails { get; }
        IProvinceRepository Province { get; }
        IRejectionDetailsRepository RejectionDetails { get; }
        ISavedJobRepository SavedJob { get; }
        ISubmissionDetailsRepository SubmissionDetails { get; }
        ITechnicalOptionsRepository TechnicalOptions { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
