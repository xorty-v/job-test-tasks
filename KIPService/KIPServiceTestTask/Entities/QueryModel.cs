namespace KIPServiceTestTask.Entities;

public class QueryModel
{
    public int Id { get; set; }
    public Guid QueryId { get; set; }
    public UserStatisticModel UserData { get; set; }
    public DateTime RequestLocalTime { get; set; }
}