using System.Text.Json.Serialization;

namespace CustomIdentityAuth.Dtos;

public class RefreshTokenRequestDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}