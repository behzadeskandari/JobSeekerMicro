using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Models;

namespace AdvertisementService.Domain.Entities
{
    public class ExceptionLog : IBaseEntity<int> , IAggregateRoot
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? ExceptionType { get; set; }
        public string? TraceId { get; set; }
        public string? HttpContextUser { get; set; }
        public string? HttpContextRequest { get; set; }
        public string? InnerException { get; set; }
    }
}

