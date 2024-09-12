using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CustomIdentityAuth.Dtos;

public class AuthTokenDto
{
    [JsonPropertyName("access_token")]
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string RefreshToken { get; set; }
    
    [JsonPropertyName("expires_at")]
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset ExpiresAt { get; set; }
}