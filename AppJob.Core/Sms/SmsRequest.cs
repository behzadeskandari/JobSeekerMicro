using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppJob.Core.Sms
{
    public class SmsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
    }
    public class SmsRequests : SmsRequest
    {
        public List<string>? Tos { get; set; }

   

    }

    public class SmsRequestBulk
    {
        public List<string> From { get; set; }
        public List<string> To { get; set; }
        public List<string> Message { get; set; }
    }


}
