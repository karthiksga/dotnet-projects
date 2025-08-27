using Domain.Authors;

namespace Domain.Books;

public class Book : IAuditableEntity
{
    public required Guid Id { get; set; }

    public required string Title { get; set; }

    public required int Year { get; set; }

    public Guid AuthorId { get; set; }

    public Author Author { get; set; } = null!;

    public DateTime CreatedAtUtc { get; set; }

    public DateTime? UpdatedAtUtc { get; set; }
}
