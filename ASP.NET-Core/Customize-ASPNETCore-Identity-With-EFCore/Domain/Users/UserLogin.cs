using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class UserLogin : IdentityUserLogin<string>
{
	public User User { get; set; }
}
