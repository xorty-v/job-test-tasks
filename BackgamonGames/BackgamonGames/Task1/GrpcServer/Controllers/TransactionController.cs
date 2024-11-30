using GrpcServer.Data;
using GrpcServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("transaction")]
    public async Task<IActionResult> MakeTransaction(long senderId, long receiverId, double amount)
    {
        var sender = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == senderId);

        var receiver = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == receiverId);

        if (sender is null || receiver is null)
            return BadRequest("Sender or receiver not found");

        if (sender.Balance < amount)
            return BadRequest("Insufficient balance");

        sender.Balance -= amount;
        receiver.Balance += amount;

        var transaction = new GameTransaction
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Amount = amount
        };

        await _dbContext.GameTransactions.AddAsync(transaction);
        await _dbContext.SaveChangesAsync();

        return Ok("Transaction successful");
    }
}