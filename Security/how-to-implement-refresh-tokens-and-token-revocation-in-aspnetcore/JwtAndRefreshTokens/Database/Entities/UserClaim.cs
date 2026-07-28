using Microsoft.AspNetCore.Identity;

namespace JwtAndRefreshTokens.Database.Entities;

public class UserClaim : IdentityUserClaim<string>
{
	public User User { get; set; }
}
