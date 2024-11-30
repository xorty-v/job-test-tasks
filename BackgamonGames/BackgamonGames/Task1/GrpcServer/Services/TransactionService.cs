using Grpc.Core;
using GrpcServer.Data;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Services;

public class TransactionService : Transaction.TransactionBase
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<GetBalanceResponse> GetBalance(GetBalanceRequest request, ServerCallContext context)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == request.UserId);

        if (user is null)
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));

        var response = new GetBalanceResponse()
        {
            Balance = user.Balance
        };

        return await Task.FromResult(response);
    }
}