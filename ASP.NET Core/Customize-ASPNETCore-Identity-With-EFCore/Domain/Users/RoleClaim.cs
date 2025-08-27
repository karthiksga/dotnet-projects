using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class RoleClaim : IdentityRoleClaim<string>
{
	public Role Role { get; set; }
}
