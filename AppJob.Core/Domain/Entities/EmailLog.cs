using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Domain.Entities
{
    public class EmailLog
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public bool IsSent { get; set; }
        public string? ErrorMessage { get; set; }
        // Add other relevant fields
    }
}
