using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobPostPublishedEvent : DomainEvent
    {
        public int JobPostId { get; }
        public int CompanyId { get; }
        public string Title { get; }
        public int JobCategoryId { get; }
        public int? ProvinceId { get; }
        public int CityId { get; }
        public string UserId { get; }
        public DateTime PublishedAt { get; }

        public JobPostPublishedEvent(int jobPostId, int companyId, string title, int jobCategoryId, int? provinceId, int cityId, string userId, DateTime publishedAt)
        {
            JobPostId = jobPostId;
            CompanyId = companyId;
            Title = title;
            JobCategoryId = jobCategoryId;
            ProvinceId = provinceId;
            CityId = cityId;
            UserId = userId;
            PublishedAt = publishedAt;
        }
    }
}

