namespace MediaItemsServer.Models
{
    public class RegisterModel : LoginModel
    {
        public IList<string> RoleList { get; set; } = new List<string>();
    }
}
