using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class CompanyUnfollowedEvent : DomainEvent
    {
        public int FollowId { get; }
        public string UserId { get; }
        public int CompanyId { get; }
        public DateTime UnfollowedAt { get; }

        public CompanyUnfollowedEvent(int followId, string userId, int companyId, DateTime unfollowedAt)
        {
            FollowId = followId;
            UserId = userId;
            CompanyId = companyId;
            UnfollowedAt = unfollowedAt;
        }
    }
}

