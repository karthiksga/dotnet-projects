using System.Security.Claims;
using Bogus;
using Domain.Authors;
using Domain.Books;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public static class DatabaseSeedService
{
	public static async Task SeedAsync(BooksDbContext dbContext, UserManager<User> userManager,
		RoleManager<Role> roleManager)
	{
		await dbContext.Database.MigrateAsync();

		if (await dbContext.Users.AnyAsync())
		{
			return;
		}

		var authors = GetAuthors(3);

		var books = GetBooks(20, authors);

		await dbContext.Authors.AddRangeAsync(authors);
		await dbContext.Books.AddRangeAsync(books);

		var adminRole = new Role { Name = "Admin" };
		var authorRole = new Role { Name = "Author" };

		var result = await roleManager.CreateAsync(adminRole);
		result = await roleManager.CreateAsync(authorRole);

		result = await roleManager.AddClaimAsync(adminRole, new Claim("users:create", "true"));
		result = await roleManager.AddClaimAsync(adminRole, new Claim("users:update", "true"));
		result = await roleManager.AddClaimAsync(adminRole, new Claim("users:delete", "true"));

		result = await roleManager.AddClaimAsync(adminRole, new Claim("books:create", "true"));
		result = await roleManager.AddClaimAsync(adminRole, new Claim("books:update", "true"));
		result = await roleManager.AddClaimAsync(adminRole, new Claim("books:delete", "true"));

		result = await roleManager.AddClaimAsync(authorRole, new Claim("books:create", "true"));
		result = await roleManager.AddClaimAsync(authorRole, new Claim("books:update", "true"));
		result = await roleManager.AddClaimAsync(authorRole, new Claim("books:delete", "true"));

		var adminUser = new User
		{
			Id = Guid.NewGuid().ToString(),
			Email = "admin@test.com",
			UserName = "admin@test.com"
		};

		result = await userManager.CreateAsync(adminUser, "Test1234!");
		result = await userManager.AddToRoleAsync(adminUser, "Admin");

		var authorUser = new User
		{
			Id = Guid.NewGuid().ToString(),
			Email = "author@test.com",
			UserName = "author@test.com"
		};

		result = await userManager.CreateAsync(authorUser, "Test1234!");
		result = await userManager.AddToRoleAsync(authorUser, "Author");

		await dbContext.SaveChangesAsync();
	}

	private static List<Author> GetAuthors(int count)
	{
		return new Faker<Author>()
			.RuleFor(x => x.Id, _ => Guid.NewGuid())
			.RuleFor(x => x.Name, f => f.Person.FullName)
			.Generate(count);
	}

	private static List<Book> GetBooks(int count, List<Author> authors)
	{
		return new Faker<Book>()
			.RuleFor(x => x.Id, _ => Guid.NewGuid())
			.RuleFor(x => x.Title, f => f.Commerce.Product())
			.RuleFor(x => x.Year, f => f.Random.Int(2018, 2025))
			.RuleFor(x => x.Author, f => f.PickRandom(authors))
			.Generate(count);
	}
}
