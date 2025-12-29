namespace JobSeeker.Shared.Contracts.Integration
{
    public class CompanyCreatedIntegrationEvent : IntegrationEvent
    {
        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public string CompanyName { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
        public string? LogoUrl { get; set; }

        public CompanyCreatedIntegrationEvent()
        {
        }

        public CompanyCreatedIntegrationEvent(int companyId, string userId, string companyName, bool isActive, bool isVerified, string? logoUrl)
        {
            CompanyId = companyId;
            UserId = userId;
            CompanyName = companyName;
            IsActive = isActive;
            IsVerified = isVerified;
            LogoUrl = logoUrl;
        }
    }
}

