using Microsoft.AspNetCore.Identity;

namespace JwtAndRefreshTokens.Database.Entities;

public class UserLogin : IdentityUserLogin<string>
{
	public User User { get; set; }
}
