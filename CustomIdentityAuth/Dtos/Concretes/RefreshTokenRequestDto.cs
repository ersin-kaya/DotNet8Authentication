using System.Text.Json.Serialization;
using CustomIdentityAuth.Dtos.Abstracts;

namespace CustomIdentityAuth.Dtos.Concretes;

public class RefreshTokenRequestDto : IRequestDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }
}