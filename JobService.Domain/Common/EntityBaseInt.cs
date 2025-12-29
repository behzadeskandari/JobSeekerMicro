using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Models;

namespace JobService.Domain.Common
{
    public abstract class EntityBaseInt : IBaseEntity<int>, IAggregateRoot
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
    }
}

