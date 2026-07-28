using Microsoft.AspNetCore.Identity;

namespace JwtAndRefreshTokens.Database.Entities;

public class Role : IdentityRole
{
	public ICollection<UserRole> UserRoles { get; set; }
	public ICollection<RoleClaim> RoleClaims { get; set; }
}
