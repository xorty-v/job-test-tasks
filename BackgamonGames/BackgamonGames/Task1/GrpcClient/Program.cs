using Grpc.Net.Client;
using GrpcServer;

var channel = GrpcChannel.ForAddress("https://localhost:5000");

try
{
    await GetAndDrawBalanceAsync(userId: 1);
    await GetAndDrawMatchesAsync();
    await JoinGameAndDrawAsync(matchId: 1, userId: 1);
    await PlayGameAndDrawAsync(matchId: 1, userId: 1, ChoiceType.K);
    await PlayGameAndDrawAsync(matchId: 1, userId: 3, ChoiceType.N);
    
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}


async Task GetAndDrawBalanceAsync(long userId)
{
    var transactionClient = new Transaction.TransactionClient(channel);
    var transactionRequest = new GetBalanceRequest { UserId = userId };

    var response = await transactionClient.GetBalanceAsync(transactionRequest);

    Console.WriteLine($"Баланс - {response.Balance}");
}

async Task GetAndDrawMatchesAsync()
{
    var matchClient = new Match.MatchClient(channel);
    var matches = await matchClient.GetMatchesAsync(new GetMatchesRequest());

    foreach (var match in matches.Matches)
    {
        if (match.FreeSlots < 2)
        {
            Console.Write($"Id - {match.MatchId}\t");
            Console.Write($"Ставка - {match.Stake}\t");
            Console.WriteLine($"Слоты - {match.FreeSlots}/2");
        }
    }
}

async Task JoinGameAndDrawAsync(long matchId, long userId)
{
    var matchClient = new Match.MatchClient(channel);
    var request = new JoinMatchRequest { MatchId = matchId, UserId = userId };

    var matches = await matchClient.JoinMatchAsync(request);

    if (matches.IsSuccessful)
        Console.WriteLine("Успешно присоединились к игре!");
}

async Task PlayGameAndDrawAsync(long matchId, long userId, ChoiceType type)
{
    var matchClient = new Match.MatchClient(channel);
    var request = new PlayGameRequest { MatchId = matchId, UserId = userId, Choice = type };

    var matches = await matchClient.PlayGameAsync(request);

    Console.WriteLine($"Результат игры: {matches.Status}");
}