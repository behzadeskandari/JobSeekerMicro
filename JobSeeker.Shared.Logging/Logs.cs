using JobSeeker.Shared.Contracts.Enums;
using JobSeeker.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Logging
{
    public class Logs : IBaseEntity<int>
    {
        public int Id { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? StatusCode { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public string HttpContextUser { get; set; }
        public string TraceId { get; set; }
        public string HttpContextResponse { get; set; }
        public string HttpContextRequest { get; set; }
        public ActivityType ActivityType { get; set; }
    }

}
