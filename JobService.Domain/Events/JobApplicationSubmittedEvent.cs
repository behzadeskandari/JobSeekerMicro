using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobApplicationSubmittedEvent : DomainEvent
    {
        public int JobApplicationId { get; }
        public int JobPostId { get; }
        public string UserId { get; }
        public string ResumeUrl { get; }
        public DateTime AppliedAt { get; }

        public JobApplicationSubmittedEvent(int jobApplicationId, int jobPostId, string userId, string resumeUrl, DateTime appliedAt)
        {
            JobApplicationId = jobApplicationId;
            JobPostId = jobPostId;
            UserId = userId;
            ResumeUrl = resumeUrl;
            AppliedAt = appliedAt;
        }
    }
}

