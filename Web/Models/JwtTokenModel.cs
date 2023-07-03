using Newtonsoft.Json;

namespace MediaItemsServer.Models
{
    public class JwtTokenModel
    {
        [JsonProperty("access_token")]
        public string BearerToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}
