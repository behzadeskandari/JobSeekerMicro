namespace JobService.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserEmail { get; }
        bool IsAuthenticated { get; }
    }
}

