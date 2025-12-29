using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class InterviewScheduledEvent : DomainEvent
    {
        public int InterviewId { get; }
        public int ApplicationId { get; }
        public DateTime InterviewDate { get; }

        public InterviewScheduledEvent(int interviewId, int applicationId, DateTime interviewDate)
        {
            InterviewId = interviewId;
            ApplicationId = applicationId;
            InterviewDate = interviewDate;
        }
    }
}

