using JobSeeker.Shared.Contracts.Integration;

namespace JobSeeker.Shared.Contracts.IntegrationEvents
{
    public class JobPostPublishedIntegrationEvent(
        int JobPostId,
        int CompanyId,
        string Title,
        int JobCategoryId,
        int? ProvinceId,
        int CityId,
        string UserId,
        DateTime PublishedAt
    ) : IntegrationEvent;
}

