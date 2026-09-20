using Microsoft.EntityFrameworkCore;
using VerticalSlicesArchitecture.Entities;

namespace VerticalSlicesArchitecture.Database;

//Primary constructor
public class UserDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public DbSet<User> Users { get; set; }
}