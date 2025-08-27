using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class UserClaim : IdentityUserClaim<string>
{
	public User User { get; set; }
}
