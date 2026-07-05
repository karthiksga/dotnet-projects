using ClaimsBasedAuthorization.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClaimsBasedAuthorization.Data;
// IdentityDbContext brings in all the Identity tables - including AspNetUserClaims,
// which is where the custom claims in this article are stored.
public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options);
