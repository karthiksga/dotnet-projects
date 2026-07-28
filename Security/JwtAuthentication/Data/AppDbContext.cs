using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JwtAuthentication.Entities;

namespace JwtAuthentication.Data;

// IdentityDbContext brings in all the Identity tables (users, roles, claims, etc.).
public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
}