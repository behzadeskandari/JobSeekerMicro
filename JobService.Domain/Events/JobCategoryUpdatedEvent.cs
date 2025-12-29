using JobSeeker.Shared.Kernel.Domain;

namespace JobService.Domain.Events
{
    public class JobCategoryUpdatedEvent : DomainEvent
    {
        public int CategoryId { get; }
        public string Name { get; }
        public DateTime UpdatedAt { get; }

        public JobCategoryUpdatedEvent(int categoryId, string name, DateTime updatedAt)
        {
            CategoryId = categoryId;
            Name = name;
            UpdatedAt = updatedAt;
        }
    }
}

