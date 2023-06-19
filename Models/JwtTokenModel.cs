namespace MediaItemsServer.Models
{
    public class JwtTokenModel
    {
        public string BearerToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
