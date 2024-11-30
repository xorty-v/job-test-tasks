using Grpc.Core;
using GrpcServer.Data;
using GrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Services;

public class MatchService : Match.MatchBase
{
    private readonly ApplicationDbContext _dbContext;

    public MatchService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<GetMatchesResponse> GetMatches(GetMatchesRequest request, ServerCallContext context)
    {
        var matches = await _dbContext.MatchHistories
            .AsNoTracking()
            .Select(match => new MatchInfo
            {
                MatchId = match.Id,
                Stake = match.Stake,
                FreeSlots = (match.FirstUserId == null ? 0 : 1) + (match.SecondUserId == null ? 0 : 1)
            })
            .ToListAsync();

        return await Task.FromResult(new GetMatchesResponse { Matches = { matches } });
    }

    public override async Task<JoinMatchResponse> JoinMatch(JoinMatchRequest request, ServerCallContext context)
    {
        var match = await _dbContext.MatchHistories.FirstOrDefaultAsync(m => m.Id == request.MatchId);

        if (match is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Match not found"));

        if (match.FirstUserId == null)
            match.FirstUserId = request.UserId;
        else if (match.SecondUserId == null)
            match.SecondUserId = request.UserId;
        else
            throw new RpcException(new Status(StatusCode.AlreadyExists,
                "Two players are already present in the match"));

        await _dbContext.SaveChangesAsync();

        return await Task.FromResult(new JoinMatchResponse { IsSuccessful = true });
    }

    public override async Task<PlayGameResponse> PlayGame(PlayGameRequest request, ServerCallContext context)
    {
        var match = await _dbContext.MatchHistories.FirstOrDefaultAsync(m => m.Id == request.MatchId);

        if (match is null || match.FirstUserId is null || match.SecondUserId is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Match not found"));

        if (match.FirstUserId != request.UserId && match.SecondUserId != request.UserId)
            throw new RpcException(new Status(StatusCode.Unknown, "Unknown Player"));

        if ((match.FirstUserId == request.UserId && match.FirstUserChoice != null) ||
            (match.SecondUserId == request.UserId && match.SecondUserChoice != null))
            throw new RpcException(new Status(StatusCode.AlreadyExists, "User has already made his move"));

        if (match.FirstUserId == request.UserId)
            match.FirstUserChoice = request.Choice;
        else if (match.SecondUserId == request.UserId)
            match.SecondUserChoice = request.Choice;

        await _dbContext.SaveChangesAsync();

        if (match.FirstUserChoice != null && match.SecondUserChoice != null)
        {
            MatchStatus winner = DetermineWinner(match.FirstUserChoice.Value, match.SecondUserChoice.Value);

            if (winner == MatchStatus.FirstUserWin)
            {
                match.WinnerId = match.FirstUserId;
                await MakeTransactionAsync(match.SecondUserId.Value, match.FirstUserId.Value, match.Stake);
                return await Task.FromResult(new PlayGameResponse { Status = MatchStatus.FirstUserWin });
            }

            if (winner == MatchStatus.SecondUserWin)
            {
                match.WinnerId = match.SecondUserId;
                await MakeTransactionAsync(match.FirstUserId.Value, match.FirstUserId.Value, match.Stake);
                return await Task.FromResult(new PlayGameResponse { Status = MatchStatus.SecondUserWin });
            }

            await _dbContext.SaveChangesAsync();

            if (winner == MatchStatus.Draw)
            {
                return await Task.FromResult(new PlayGameResponse { Status = MatchStatus.Draw });
            }
        }

        return await Task.FromResult(new PlayGameResponse { Status = MatchStatus.WaitingForOpponent });
    }

    private async Task MakeTransactionAsync(long senderId, long receiverId, double amount)
    {
        var sender = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == senderId);
        var receiver = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == receiverId);

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
    }

    private MatchStatus DetermineWinner(ChoiceType choice1, ChoiceType choice2)
    {
        if (choice1 == choice2)
            return MatchStatus.Draw;

        if ((choice1 == ChoiceType.K && choice2 == ChoiceType.N) ||
            (choice1 == ChoiceType.N && choice2 == ChoiceType.B) ||
            (choice1 == ChoiceType.B && choice2 == ChoiceType.K))
            return MatchStatus.FirstUserWin;

        return MatchStatus.SecondUserWin;
    }
}