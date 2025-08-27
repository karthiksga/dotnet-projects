using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class Role : IdentityRole
{
	public ICollection<UserRole> UserRoles { get; set; }
	public ICollection<RoleClaim> RoleClaims { get; set; }
}
