using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class CompanyCreatedEvent : DomainEvent
    {
        public int CompanyId { get; }
        public string Name { get; }
        public string UserId { get; }
        public string? LogoUrl { get; }
        public DateTime CreatedAt { get; }
        public bool isActive { get; set; }
        public bool IsVerified { get; set; }
        public CompanyCreatedEvent(int companyId, string name, string userId, string? logoUrl, DateTime createdAt, bool isActive, bool isVerified)
        {
            CompanyId = companyId;
            Name = name;
            UserId = userId;
            LogoUrl = logoUrl;
            CreatedAt = createdAt;
            this.isActive = isActive;
            IsVerified = isVerified;
        }
    }
}

