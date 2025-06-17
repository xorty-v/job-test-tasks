using System.Security.Claims;
using backend.Contracts.Auth;
using backend.Entities;
using backend.Infrastructure;
using backend.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace backend.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", Login);
        app.MapPost("/auth/refresh-token", RefreshToken);
        app.MapPost("/auth/revoke", Revoke).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] ApplicationDbContext dbContext,
        [FromServices] IOptions<JwtOptions> jwtOptions,
        [FromServices] ITokenProvider tokenProvider,
        [FromServices] IPasswordHasher passwordHasher)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == request.Email);

        if (user is null)
        {
            return Results.NotFound();
        }

        var result = passwordHasher.Verify(request.Password, user.PasswordHash);

        if (result == false)
        {
            return Results.Unauthorized();
        }

        var accessToken = tokenProvider.GenerateAccessToken(user);

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = tokenProvider.GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshExpiresDays)
        };

        dbContext.RefreshToken.Add(refreshToken);
        await dbContext.SaveChangesAsync();

        return Results.Ok(new TokenResponse(accessToken, refreshToken.Token));
    }

    private static async Task<IResult> RefreshToken(
        [FromBody] RefreshRequest request,
        [FromServices] ITokenProvider tokenProvider,
        [FromServices] IOptions<JwtOptions> jwtOptions,
        [FromServices] ApplicationDbContext dbContext)
    {
        var refreshToken = await dbContext.RefreshToken
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == request.RefreshToken);

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Results.BadRequest("The refresh token has expired");
        }

        string accessToken = tokenProvider.GenerateAccessToken(refreshToken.User);

        refreshToken.Token = tokenProvider.GenerateRefreshToken();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(jwtOptions.Value.RefreshExpiresDays);
        await dbContext.SaveChangesAsync();

        return Results.Ok(new TokenResponse(accessToken, refreshToken.Token));
    }

    private static async Task<IResult> Revoke(
        [FromBody] RevokeRequest request,
        HttpContext httpContext,
        [FromServices] ApplicationDbContext dbContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Results.Unauthorized();
        }

        var refreshToken = await dbContext.RefreshToken
            .FirstOrDefaultAsync(r => r.Token == request.RefreshToken && r.UserId == userId);

        if (refreshToken is null)
        {
            return Results.Unauthorized();
        }

        dbContext.RefreshToken.Remove(refreshToken);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}