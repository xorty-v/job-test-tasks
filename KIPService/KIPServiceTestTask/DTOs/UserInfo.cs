using System.Text.Json.Serialization;

namespace KIPServiceTestTask.DTOs;

public class UserInfo
{
    [JsonPropertyName("user_id")] 
    public Guid UserId { get; }

    [JsonPropertyName("count_sign_in")] 
    public int CountSignIn { get; }

    public UserInfo(Guid userId, int countSignIn)
    {
        UserId = userId;
        CountSignIn = countSignIn;
    }
}