using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Common.Errors
{
    public interface IServiceException
    {
        public string Message { get; }
        public HttpStatusCode StatusCode { get; }

    }
}
