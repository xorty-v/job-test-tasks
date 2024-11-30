namespace GrpcServer.Models;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }

    public List<MatchHistory> MatchHistoriesFirstUser { get; set; }
    public List<MatchHistory> MatchHistoriesSecondUser { get; set; }
    public List<MatchHistory> WonMatches { get; set; }
    public List<GameTransaction> SenderTransactions { get; set; }
    public List<GameTransaction> ReceiverTransactions { get; set; }
}