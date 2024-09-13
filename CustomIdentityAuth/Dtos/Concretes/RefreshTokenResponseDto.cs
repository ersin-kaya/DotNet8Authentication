using System.Text.Json.Serialization;

namespace CustomIdentityAuth.Dtos.Concretes;

public class RefreshTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
    
    [JsonPropertyName("expires_at")]
    public DateTimeOffset ExpiresAt { get; set; }
}