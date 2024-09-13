using System.Text.Json.Serialization;

namespace CustomIdentityAuth.Dtos.Concretes;

public class AccessTokenDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("expires_at")]
    public DateTimeOffset ExpiresAt { get; set; }
}