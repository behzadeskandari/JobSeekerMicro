using System.Threading;
using System.Threading.Tasks;

namespace AdvertisementService.Application.Interfaces
{
    public interface IIdentityServiceClient
    {
        /// <summary>
        /// Gets the owner userId for a company from JobService
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>Owner userId or null if company not found</returns>
        Task<string?> GetCompanyOwnerUserIdAsync(int companyId, CancellationToken ct = default);

        /// <summary>
        /// Checks if a user exists in IdentityService
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns>True if user exists, false otherwise</returns>
        Task<bool> UserExistsAsync(string userId, CancellationToken ct = default);
    }
}

