using JwtAndRefreshTokens.Features.Authorization.Models;
using JwtAndRefreshTokens.Features.Shared;

namespace JwtAndRefreshTokens.Features.Authorization;

public interface IClientAuthorizationService
{
    /// <summary>
    /// Updates a user's role and invalidates their refresh tokens
    /// </summary>
    /// <param name="userId">The ID of the user to update</param>
    /// <param name="newRole">The new role to assign to the user</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A result containing success or failure information</returns>
    Task<Result<string>> UpdateUserRoleAsync(string userId, string newRole, CancellationToken cancellationToken = default);
	Task<Result<LoginResponse>> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
	Task<Result<RefreshTokenResponse>> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
}
