using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class UserToken : IdentityUserToken<string>
{
	public User User { get; set; }
}
