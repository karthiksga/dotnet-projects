using Microsoft.AspNetCore.Identity;

namespace JwtAndRefreshTokens.Database.Entities;

public class RoleClaim : IdentityRoleClaim<string>
{
	public Role Role { get; set; }
}
