using System.Text.Json.Serialization;

namespace FSight.API.Dtos.Identity
{
    public class GoogleUserDto
    {
        [JsonPropertyName("idToken")]
        public string IdToken { get; set; }
    }
}