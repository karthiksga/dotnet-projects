namespace JwtAndRefreshTokens.Database.Entities;

public class RefreshToken : IAuditableEntity
{
	public string Token { get; set; } = null!;

	public string JwtId { get; set; } = null!;

	public DateTime ExpiryDate { get; set; }

	public bool Invalidated { get; set; }

	public string UserId { get; set; } = null!;

	public User User { get; set; } = null!;

	public DateTime CreatedAtUtc { get; set; }
	public DateTime? UpdatedAtUtc { get; set; }
}
