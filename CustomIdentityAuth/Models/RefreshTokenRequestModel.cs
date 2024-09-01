using System.Text.Json.Serialization;

namespace CustomIdentityAuth.Models;

public class RefreshTokenRequestModel
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}