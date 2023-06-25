using MediaItemsServer.Interfaces;
using MediaItemsServer.Models;

namespace MediaItemsServer.Services
{
    public class RoleService : IRoleService
    {
        private readonly DbContext _dbContext;

        public RoleService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IList<UserRole> Roles => _dbContext.Roles;

        public IList<UserRole> GetRolesForUser(string userName)
        {
            return _dbContext.Users.Where(x => x.Name == userName)
                .Join(_dbContext.SecurityRelations, user => user.Id, relation => relation.ParentId, (user, relation) => relation)
                .Join(Roles, relation => relation.ChildId, role => role.Id, (relation, role) => role)
                .ToList();
        }
    }
}
