using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Domain.Entities
{
    public class GeneratedLink
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Token { get; set; }
        public string Purpose { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; }
        public string? AssociatedData { get; set; } // Store associated data as JSON
    }
}
