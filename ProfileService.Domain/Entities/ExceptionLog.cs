using JobSeeker.Shared.Kernel.Abstractions;
using JobSeeker.Shared.Models;

namespace ProfileService.Domain.Entities
{
    public class ExceptionLog : IBaseEntity<int> , IAggregateRoot
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public string? ExceptionType { get; set; }
        public string? ClassName { get; set; }
        public string? MethodName { get; set; }
        public string? TraceId { get; set; }
        public string? HttpContextUser { get; set; }
        public string? HttpContextRequest { get; set; }
        public string? InnerException { get; set; }
    }
}

