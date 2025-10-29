using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Contracts.Enums
{
    public enum JobRequestStatus
    {
        Start = 1,
        Sended ,
        Read,
        Pending,
        Reviewed,
        Shortlisted,
        Interviewing,
        Offered,
        Rejected,
        Accepted,
    }
}
