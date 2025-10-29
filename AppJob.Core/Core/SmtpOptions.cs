using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Core
{
    public class SmtpOptions
    {
        public string? Host { get; set; }
        public int Port { get; set; } = 587; // Default SMTP port
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool UseSsl { get; set; } = true;
        public string? DefaultFromAddress { get; set; }
        public string? DefaultFromName { get; set; }
    }
}
