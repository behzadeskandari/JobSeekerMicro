using Kavenegar.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Domain.Entities
{
    public class SMSLog
    {
        public List<string> from { get; set; }
        public List<string> to { get; set; }
        public List<string> messages{ get; set; }
        public int Id { get; set; }

    }


    public class SendResult
    {
        public int Id { get; set; }
        public long Messageid { get; set; }

        public int Cost { get; set; }

        public long Date { get; set; }

        public string Message { get; set; }

        public string Receptor { get; set; }

        public string Sender { get; set; }

        public int Status { get; set; }

        public string StatusText { get; set; }
    }
}
