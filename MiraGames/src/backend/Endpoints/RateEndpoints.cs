using backend.Contracts.Rates;
using backend.Entities;
using backend.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Endpoints;

public static class RateEndpoints
{
    public static IEndpointRouteBuilder MapRateEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/rate", Get);
        app.MapPost("/rate", Update).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> Get(
        [FromServices] ApplicationDbContext dbContext)
    {
        var rate = await dbContext.Rates
            .OrderByDescending(r => r.UpdatedAtOnUtc)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return rate is null ? Results.NotFound() : Results.Ok(rate.Value);
    }

    private static async Task<IResult> Update(
        [FromBody] UpdateRateRequest request,
        [FromServices] ApplicationDbContext dbContext)
    {
        var rate = new Rate
        {
            Id = Guid.NewGuid(),
            Value = request.Value,
            UpdatedAtOnUtc = DateTime.UtcNow
        };

        dbContext.Rates.Add(rate);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}