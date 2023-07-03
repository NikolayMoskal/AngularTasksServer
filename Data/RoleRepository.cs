using MediaItemsServer.Data.Contracts;
using MediaItemsServer.Models;

namespace MediaItemsServer.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _dbContext;
        private static readonly object Lock = new object();

        public RoleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IList<UserRole> Roles => _dbContext.Roles;

        public IList<UserRole> GetRolesForUser(string userName)
        {
            return _dbContext.Users.Where(x => x.Name.Equals(userName, StringComparison.InvariantCultureIgnoreCase))
                .Join(_dbContext.SecurityRelations, user => user.Id, relation => relation.ChildId, (user, relation) => relation)
                .Join(Roles, relation => relation.ParentId, role => role.Id, (relation, role) => role)
                .ToList();
        }

        public void Save(UserRole userRole)
        {
            lock (Lock)
            {
                if (Roles.Any(x => x.RoleName.Equals(userRole.RoleName, StringComparison.InvariantCultureIgnoreCase)))
                    return;

                userRole.Id = Roles.Count;
                Roles.Add(userRole);
            }
        }

        public void Delete(UserRole userRole)
        {
            lock (Lock)
            {
                Roles.ToList().RemoveAll(x =>
                    x.RoleName.Equals(userRole.RoleName, StringComparison.InvariantCultureIgnoreCase));
            }
        }
    }
}
