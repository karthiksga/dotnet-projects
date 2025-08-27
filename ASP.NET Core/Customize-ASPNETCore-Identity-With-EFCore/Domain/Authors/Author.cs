using Domain.Books;
using Domain.Users;

namespace Domain.Authors;

public class Author : IAuditableEntity
{
    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public List<Book> Books { get; set; } = [];

    public string? UserId { get; set; }

    public User? User { get; set; }

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }
}
