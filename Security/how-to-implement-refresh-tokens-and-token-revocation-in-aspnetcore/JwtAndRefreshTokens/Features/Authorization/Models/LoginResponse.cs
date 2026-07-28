namespace JwtAndRefreshTokens.Features.Authorization.Models;

public record LoginResponse(string Token, string RefreshToken);
