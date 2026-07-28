using JwtAndRefreshTokens.Features.Books.Shared;

namespace JwtAndRefreshTokens.Features.Authors.Shared;

public sealed record AuthorResponse(Guid Id, string Name, List<BookResponse> Books);
