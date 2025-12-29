using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Models;

namespace JobService.Domain.Common
{
    public abstract class EntityBase<TKey> : IBaseEntity<TKey> , IAggregateRoot
    {
        public TKey Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}

