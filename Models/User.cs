namespace MediaItemsServer.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not User user) return false;

            return Id == user.Id
                   && Name == user.Name
                   && Email == user.Email
                   && Password == user.Password
                   && RefreshToken == user.RefreshToken
                   && RefreshTokenExpiryTime == user.RefreshTokenExpiryTime;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email, Password, RefreshToken, RefreshTokenExpiryTime);
        }
    }
}
