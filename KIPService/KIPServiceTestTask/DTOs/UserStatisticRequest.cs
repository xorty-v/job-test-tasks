using System.Text.Json.Serialization;

namespace KIPServiceTestTask.DTOs;

public class UserStatisticRequest
{
    [JsonPropertyName("user_id")] public Guid UserId { get; }

    [JsonPropertyName("time_in")] public DateTime TimeIn { get; }

    [JsonPropertyName("time_out")] public DateTime TimeOut { get; }

    public UserStatisticRequest(Guid userId, DateTime timeIn, DateTime timeOut)
    {
        UserId = userId;
        TimeIn = timeIn;
        TimeOut = timeOut;
    }
}