using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Models;

namespace JobService.Domain.Entities
{
    public class Log : IBaseEntity<int>, IAggregateRoot
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public int? StatusCode { get; set; }
        public string? Title { get; set; }
        public string? Detail { get; set; }
        public string? TraceId { get; set; }
        public string? HttpContextUser { get; set; }
        public string? HttpContextRequest { get; set; }
        public string? HttpContextResponse { get; set; }
    }
}

