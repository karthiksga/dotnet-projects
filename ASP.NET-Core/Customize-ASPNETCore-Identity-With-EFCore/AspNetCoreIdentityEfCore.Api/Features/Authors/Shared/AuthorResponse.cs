using AspNetCoreIdentityEfCore.Api.Features.Books.Shared;

namespace AspNetCoreIdentityEfCore.Api.Features.Authors.Shared;

public sealed record AuthorResponse(Guid Id, string Name, List<BookResponse> Books);
