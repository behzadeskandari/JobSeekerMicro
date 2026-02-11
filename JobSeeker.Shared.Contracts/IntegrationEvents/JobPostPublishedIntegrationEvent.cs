using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class JobPostPublishedIntegrationEvent : IntegrationEvent
    {
        public int JobPostId { get; set; }
        public int CompanyId { get; set; }

        public string Title { get; set; }
        public int JobCategoryId { get; set; }
        public int? ProvinceId { get; set; }
        public int CityId { get; set; }
        public string UserId { get; set; }
        public DateTime PublishedAt { get; set; }
        public JobPostPublishedIntegrationEvent()
        {

        }
        public JobPostPublishedIntegrationEvent(
       int jobPostId,
       int companyId,
       string title,
       int jobCategoryId,
       int? provinceId,
       int cityId,
       string userId,
       DateTime publishedAt)
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
    };
}

