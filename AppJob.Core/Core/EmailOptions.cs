using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Core
{
    public class EmailOptions
    {
        public SmtpOptions? Smtp { get; set; }
        // Add other email provider options if needed
    }
}
