using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Core
{
    public class EmailMessage
    {
        public Guid Id { get; set; }
        public List<EmailRecipient> To { get; set; } = new List<EmailRecipient>();
        public List<EmailRecipient> Cc { get; set; } = new List<EmailRecipient>();
        public List<EmailRecipient> Bcc { get; set; } = new List<EmailRecipient>();
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<string> Attachments { get; set; } = new List<string>(); // File paths
        // You might want to include a way to pass Stream for attachments later
    }
}
