using System.Security.Claims;

namespace AssessmentService.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserEmail { get; }
        bool IsAuthenticated { get; }
        IEnumerable<Claim>? Claims { get; }
    }
}

