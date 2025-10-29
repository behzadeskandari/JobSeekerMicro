using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Common.Errors
{
    public class UserError : IError
    {
        public List<IError> Reasons { get; set; } = new List<IError>();

        public string Message { get; set; } = ErrorMessages.UserNotFound;

        public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    }
}
