using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
        public string Type { get; set; } = default!;
        public string Content { get; set; } = default!; // serialized payload
        public bool Published { get; set; } = false;
    }
}
