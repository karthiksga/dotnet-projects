using Domain.Authors;
using Domain.Books;
using Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class BooksDbContext : IdentityDbContext<User, Role, string,
			UserClaim, UserRole, UserLogin,
			RoleClaim, UserToken>
{
	public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
	{
	}

	public DbSet<Author> Authors { get; set; } = null!;
	public DbSet<Book> Books { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.HasDefaultSchema(DatabaseConsts.Schema);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksDbContext).Assembly);
	}

	public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
	{
		return await base.SaveChangesAsync(cancellationToken);
	}
}
