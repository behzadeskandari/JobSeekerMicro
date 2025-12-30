using System.Security.Claims;

namespace ProfileService.Infrastructure.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserEmail { get; }
        bool IsAuthenticated { get; }
        IEnumerable<Claim>? Claims { get; }
    }
}

