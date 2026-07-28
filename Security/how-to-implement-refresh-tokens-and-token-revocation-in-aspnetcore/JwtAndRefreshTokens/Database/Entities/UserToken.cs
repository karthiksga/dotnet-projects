using Microsoft.AspNetCore.Identity;

namespace JwtAndRefreshTokens.Database.Entities;

public class UserToken : IdentityUserToken<string>
{
	public User User { get; set; }
}
