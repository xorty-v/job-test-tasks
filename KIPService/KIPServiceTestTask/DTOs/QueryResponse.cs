using System.Text.Json.Serialization;

namespace KIPServiceTestTask.DTOs;

public class QueryResponse
{
    [JsonPropertyName("query")] public Guid QueryId { get; }

    [JsonPropertyName("percent")] public int Percent { get; }

    [JsonPropertyName("result")] public UserInfo? Result { get; }

    public QueryResponse(Guid queryId, int percent, UserInfo? result)
    {
        QueryId = queryId;
        Percent = percent;
        Result = result;
    }
}