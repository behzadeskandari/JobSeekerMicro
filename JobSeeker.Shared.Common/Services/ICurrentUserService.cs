using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Shared.Common.Services
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        string UserEmail { get; }
        bool IsAuthenticated { get; }
        IList<string> Roles { get; }
        bool IsInRole(string role);
    }
}
