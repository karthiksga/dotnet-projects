
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System;
using System.Text;
using global::JwtAuthApp.Model;

namespace JwtAuthApp.JWT;

public static class JwtAuthBuilderExtesnions
{
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration(configuration);

        services.AddAuthorization();

        return services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.SaveToken = true; // Defines whether the bearer token should be stored in the AuthenticationProperties after a successful authorization.
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, // Ensures that the token’s iss (issuer) claim matches the expected issuer.
                ValidateAudience = true, // Validates the aud (audience) claim in the token to make sure it matches the expected audience.
                ValidateLifetime = true, // Checks that the token’s exp (expiration) time is valid, the token hasn't expired.
                ValidateIssuerSigningKey = true, // Verifies the token’s signature against the signing key to ensure its integrity.
                ValidIssuer = jwtConfiguration.Issuer,// Specifies the expected issuer for the token, pulled from the configuration (appsettings.json or environment variables).
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret)), // Uses a symmetric security key to sign and validate the JWT, converting the secret key from the configuration into a byte array for encryption.

                RequireExpirationTime = true, // Ensures that the JWT token contains the exp (expiration) claim.
            };
            x.Events = new JwtBearerEvents
            {
                OnMessageReceived = context => // save token in context
                {
                    string authorization = context.Request.Headers["Authorization"];

                    if (string.IsNullOrEmpty(authorization))
                    {
                        context.NoResult();
                    }
                    else
                    {
                        context.Token = authorization.Replace("Bearer ", string.Empty);

                        var isValidToken = IsTokenWellFormed(context.Token);
                    }

                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception is SecurityTokenExpiredException)
                    {
                        context.Response.Headers.Add("Token-Expired", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }

    public static bool IsTokenWellFormed(string token)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;

        var parts = token.Split('.');
        return parts.Length == 3; // A well-formed JWT should have exactly three parts
    }
}
