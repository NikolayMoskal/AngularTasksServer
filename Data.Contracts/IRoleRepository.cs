using MediaItemsServer.Models;

namespace MediaItemsServer.Data.Contracts
{
    public interface IRoleRepository
    {
        IList<UserRole> GetRolesForUser(string userName);
        void Save(UserRole userRole);
        void Delete(UserRole userRole);
    }
}
