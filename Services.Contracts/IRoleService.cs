using MediaItemsServer.Models;

namespace MediaItemsServer.Services.Contracts
{
    public interface IRoleService
    {
        IList<UserRole> GetRolesForUser(string userName);
        void Save(UserRole userRole);
        void Delete(UserRole userRole);
    }
}
