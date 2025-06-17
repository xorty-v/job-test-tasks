using backend.Contracts.Payments;
using backend.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Endpoints;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/payments", GetPayments).RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetPayments(
        [FromServices] ApplicationDbContext dbContext,
        [FromQuery] int take = 5)
    {
        var payments = await dbContext.Payments
            .Include(p => p.Client)
            .OrderByDescending(p => p.TimestampOnUtc)
            .Take(take)
            .AsNoTracking()
            .Select(p => new PaymentResponse( p.Client.Email, p.Amount, p.TimestampOnUtc))
            .ToListAsync();

        return Results.Ok(payments);
    }
}