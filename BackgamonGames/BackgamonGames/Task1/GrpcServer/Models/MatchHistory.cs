namespace GrpcServer.Models;

public class MatchHistory
{
    public long Id { get; set; }

    public long? FirstUserId { get; set; }
    public User? FirstUser { get; set; }

    public long? SecondUserId { get; set; }
    public User? SecondUser { get; set; }

    public double Stake { get; set; }

    public ChoiceType? FirstUserChoice { get; set; }

    public ChoiceType? SecondUserChoice { get; set; }

    public long? WinnerId { get; set; }
    public User? WinnerUser { get; set; }
}