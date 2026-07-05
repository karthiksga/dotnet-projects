using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RoleBasedAuthorization.Entities;

namespace RoleBasedAuthorization.Data;

// IdentityDbContext brings in all the Identity tables (users, roles, claims, etc.).
public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options);