using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class CompanyFollowedEvent : DomainEvent
    {
        public int FollowId { get; }
        public string UserId { get; }
        public int CompanyId { get; }
        public DateTime FollowedAt { get; }

        public CompanyFollowedEvent(int followId, string userId, int companyId, DateTime followedAt)
        {
            FollowId = followId;
            UserId = userId;
            CompanyId = companyId;
            FollowedAt = followedAt;
        }
    }
}

