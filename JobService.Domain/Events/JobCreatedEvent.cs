using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobCreatedEvent : DomainEvent
    {
        public int JobId { get; }
        public string Title { get; }
        public int CompanyId { get; }
        public int JobCategoryId { get; }
        public int? CityId { get; }
        public DateTime CreatedAt { get; }

        public JobCreatedEvent(int jobId, string title, int companyId, int jobCategoryId, int? cityId, DateTime createdAt)
        {
            JobId = jobId;
            Title = title;
            CompanyId = companyId;
            JobCategoryId = jobCategoryId;
            CityId = cityId;
            CreatedAt = createdAt;
        }
    }
}

