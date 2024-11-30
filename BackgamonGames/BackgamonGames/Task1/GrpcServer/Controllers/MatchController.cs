using GrpcServer.Data;
using GrpcServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public MatchController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("match")]
    public async Task<IActionResult> CreateMatch(long userId, double stake)
    {
        var firstUser = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (firstUser is null)
            return BadRequest("User not found");

        if (firstUser.Balance < stake)
            return BadRequest("Insufficient balance for the stake");

        var match = new MatchHistory
        {
            FirstUserId = userId,
            Stake = stake
        };

        await _dbContext.MatchHistories.AddAsync(match);
        await _dbContext.SaveChangesAsync();

        return Ok("Match created successfully");
    }
}