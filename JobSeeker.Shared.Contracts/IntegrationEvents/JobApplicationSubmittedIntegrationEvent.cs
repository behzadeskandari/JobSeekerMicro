using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class JobApplicationSubmittedIntegrationEvent : IntegrationEvent
    {
        public JobApplicationSubmittedIntegrationEvent()
        {
                
        }
        public JobApplicationSubmittedIntegrationEvent(int jobApplicationId, int jobPostId, string userId, string resumeUrl, DateTime appliedAt)
        {
            JobApplicationId = jobApplicationId;
            JobPostId = jobPostId;
            UserId = userId;
            ResumeUrl = resumeUrl;
            AppliedAt = appliedAt;
        }

       public int JobApplicationId { get; set; }
       public int JobPostId { get; set; }
       public string UserId { get; set; }
       public string ResumeUrl { get; set; }
       public DateTime AppliedAt { get; set; }

    } 
}

