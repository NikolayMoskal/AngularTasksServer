using System.Linq.Expressions;
using MediaItemsServer.Models;

namespace MediaItemsServer.Data.Contracts
{
    public interface IRoleRepository
    {
        IList<UserRole> GetAll(Expression<Func<UserRole, bool>> filter = null);
        IList<UserRole> GetRolesForUser(string userName);
        void Save(UserRole userRole);
        void Delete(UserRole userRole);
    }
}
