using backend.Contracts.Clients;
using backend.Entities;
using backend.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Endpoints;

public static class ClientEndpoints
{
    public static IEndpointRouteBuilder MapClientEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/clients").RequireAuthorization();

        group.MapGet("", GetClients);
        group.MapPost("", Add);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetClients(
        [FromServices] ApplicationDbContext dbContext)
    {
        var clients = await dbContext.Clients.AsNoTracking().ToListAsync();

        return Results.Ok(clients);
    }

    private static async Task<IResult> Add(
        [FromBody] AddClientRequest request,
        [FromServices] ApplicationDbContext dbContext)
    {
        var client = new Client()
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
        };

        dbContext.Clients.Add(client);
        await dbContext.SaveChangesAsync();

        return Results.Created($"/clients/{client.Id}", request);
    }

    private static async Task<IResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateClientRequest request,
        [FromServices] ApplicationDbContext dbContext)
    {
        var existingClient = await dbContext.Clients.SingleOrDefaultAsync(c => c.Id == id);

        if (existingClient is null)
        {
            return Results.NotFound();
        }

        existingClient.Name = request.Name;
        existingClient.Email = request.Email;
        existingClient.BalanceT = request.BalanceT;

        await dbContext.SaveChangesAsync();
        return Results.NoContent();
    }

    private static async Task<IResult> Delete(
        [FromRoute] Guid id,
        [FromServices] ApplicationDbContext dbContext)
    {
        var client = await dbContext.Clients.SingleOrDefaultAsync(c => c.Id == id);

        if (client is null)
        {
            return Results.NotFound();
        }

        dbContext.Clients.Remove(client);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }
}