using Microsoft.AspNetCore.Identity;
using PolicyBasedAuthorization.Entities;
using PolicyBasedAuthorization.Services;
using System.Security.Claims;

namespace PolicyBasedAuthorization.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var subscriptions = scope.ServiceProvider.GetRequiredService<SubscriptionStore>();
        // Ensure roles exist
        foreach (var role in new[] { Roles.Admin, Roles.User, Roles.Manager })
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Admin: STANDARD tier (pro access comes from the Admin role, not subscription).
        // Long tenure - joined over two years ago.
        await CreateUserAsync(userManager, subscriptions,
            email: "admin@codewithmukesh.com",
            password: "Admin123!",
            firstName: "Default",
            lastName: "Admin",
            roles: [Roles.Admin, Roles.Manager],
            department: "engineering",
            joined: "2024-01-15",
            subscriptionTier: "standard");

        // Manager: PREMIUM tier (pro access via subscription), but joined recently -
        // fails the 6-month tenure check.
        await CreateUserAsync(userManager, subscriptions,
            email: "manager@codewithmukesh.com",
            password: "Manager123!",
            firstName: "Default",
            lastName: "Manager",
            roles: [Roles.Manager],
            department: "operations",
            joined: "2026-04-20",
            subscriptionTier: "premium");

        // Regular user: free tier, no elevated roles - but a long-time member.
        await CreateUserAsync(userManager, subscriptions,
            email: "user@codewithmukesh.com",
            password: "User123!",
            firstName: "Default",
            lastName: "User",
            roles: [Roles.User],
            department: null,
            joined: "2024-08-01",
            subscriptionTier: "free");
    }
    private static async Task CreateUserAsync(
        UserManager<ApplicationUser> userManager,
        SubscriptionStore subscriptions,
        string email,
        string password,
        string firstName,
        string lastName,
        string[] roles,
        string? department,
        string joined,
        string subscriptionTier
    )
    {
        if(await userManager.FindByEmailAsync(email) != null)
        {
            return; // User already exists
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            EmailConfirmed = true,
        };

        var createUserResult = await userManager.CreateAsync(user, password);
        if(!createUserResult.Succeeded)
        {
            throw new Exception($"Failed to create user {email}: {string.Join(", ", createUserResult.Errors.Select(e => e.Description))}");
        }
        var addtoRolesResult = await userManager.AddToRolesAsync(user, roles);
        if(!addtoRolesResult.Succeeded)
        {
            throw new Exception($"Failed to add roles to user {email}: {string.Join(", ", addtoRolesResult.Errors.Select(e => e.Description))}");
        }
        var claims = new List<Claim> { new(AppClaimTypes.Joined, joined) };
        if (department is not null)
        {
            claims.Add(new Claim(AppClaimTypes.Department, department));
        }
        var addClaimsResult  = await userManager.AddClaimsAsync(user, claims);
        if(!addClaimsResult.Succeeded)
        {
            throw new Exception($"Failed to add claims to user {email}: {string.Join(", ", addClaimsResult.Errors.Select(e => e.Description))}");
        }

        // Subscription tier lives OUTSIDE Identity - claims transformation reads it per request.
        subscriptions.SetTier(user.Id, subscriptionTier);
    }
}
