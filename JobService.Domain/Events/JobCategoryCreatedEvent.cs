using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobCategoryCreatedEvent : DomainEvent
    {
        public int CategoryId { get; }
        public string Name { get; }
        public string Industry { get; }
        public DateTime CreatedAt { get; }

        public JobCategoryCreatedEvent(int categoryId, string name, string industry, DateTime createdAt)
        {
            CategoryId = categoryId;
            Name = name;
            Industry = industry;
            CreatedAt = createdAt;
        }
    }
}

