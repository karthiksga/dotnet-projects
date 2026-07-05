using RoleBasedAuthorization.Entities;

namespace RoleBasedAuthorization.Auth;

public interface ITokenService
{
    // Builds a signed JWT for the given user and returns it along with its expiry time.
    (string Token, DateTime ExpiresAt) CreateToken(ApplicationUser user, IEnumerable<string> roles);
}