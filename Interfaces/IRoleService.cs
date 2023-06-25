using MediaItemsServer.Models;

namespace MediaItemsServer.Interfaces
{
    public interface IRoleService
    {
        IList<UserRole> GetRolesForUser(string userName);
    }
}
